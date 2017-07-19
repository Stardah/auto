using ICSharpCode.SharpZipLib.Tar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Rpi
{
    public partial class Form1 : Form
    {
        private const int DefaultServerPort = 6400;
        private const int DefaultClientPort = 6401;
        private const int Cooldown = 1000;
#if DEBUG
        public const string TemporaryPrefix = @"temp";
#else
        public const string TemporaryPrefix = @"/home/pi/Debug/temp";
#endif

        private Client m_client;
        private bool m_listen = true;
        private bool m_finish = false;
        private int unique = 0;
        private List<Order> m_orders = new List<Order>();
        private HashSet<int> m_pulled = new HashSet<int>();

        public Form1()
        {
            InitializeComponent();
            Logger.CreateLogFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Очистка временных директорий.
            string name;
            int i = 0;

            var cur = Directory.GetCurrentDirectory();

            while (Directory.Exists(name = (TemporaryPrefix + i++.ToString())))
            {
                Directory.Delete(name, true);
            }

            m_client = new Client(DefaultClientPort);
            m_client.Start();

            Text = GetLocalIpAddress();

            Thread thread = new Thread(() =>
            {
                while (m_listen)
                {
                    if (!m_client.Connected)
                    {
                        Thread.Sleep(Cooldown);
                        continue;
                    }

                    if (m_finish)
                    {
                        var reply = m_client.SendString(MessageType.Finish, true, m_orders[0].Id);

                        if (Message.GetMessageType(reply) == MessageType.Ack)
                        {
                            m_pulled.Remove(m_orders[0].Id);
                            m_orders.RemoveAt(0);
                        }

                        m_finish = false;
                        Thread.Sleep(Cooldown);

                        continue;
                    }

                    var req = m_client.SendString(MessageType.RequestIds);

                    if (req == null)
                        continue;

                    var ids = (
                        from v
                        in req.Split(' ', '$')
                        where v.Length > 0
                        select v
                        ).ToArray();

                    foreach (var v in ids)
                    {
                        int id = int.Parse(v);

                        if (!m_pulled.Contains(id))
                        {
                            m_pulled.Add(id);
                            m_client.SendString(MessageType.RequestData, false, v);
                            ProcessData(m_client.PullBytes());
                        }
                    }

                    WriteToDrive();
                }
            });

            thread.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_listen = false;
            m_client.Stop();
        }

        private void autoUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (m_client.Connected)
            {
                connectionLabel.Text = "Соединено";
                connectionLabel.ForeColor = Color.Green;
            }
            else
            {
                connectionLabel.Text = "Нет соединения";
                connectionLabel.ForeColor = Color.Red;
            }

            if (m_orders.Count == 0)
            {
                writtenLabel.Text = "УП отсутствуют";
                writtenLabel.ForeColor = Color.Black;
            }
            else if (m_orders[0].Written)
            {
                writtenLabel.Text = "Программа записана";
                writtenLabel.ForeColor = Color.Blue;
            }
            else
            {
                writtenLabel.Text = "Ожидание USB-носителя...";
                writtenLabel.ForeColor = Color.Orange;
            }

            currentLabel.Text =
                "Текущий заказ:\n\r" + m_orders.FirstOrDefault()?.ToString();
            nextLabel.Text =
                "Следующий заказ:\n\r" + m_orders.Skip(1).FirstOrDefault()?.ToString();

            // logTextBox.Text = Logger.GetLogString();
            // logTextBox.Select(logTextBox.TextLength, 0);
            // logTextBox.ScrollToCaret();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '#' && m_orders.Count > 0)
            {
                m_finish = true;
            }
            else if (e.KeyChar == '*')
            {
                UpdateProgram();
            }
        }

        private void WriteToDrive()
        {
            if (m_orders.Count > 0 && m_orders[0].Prog != null)
            {
                m_orders[0].Written =
#if DEBUG
                WriteToDriveWindows();
#else
                WriteToDriveLinux();
#endif
            }
        }

        private bool WriteToDriveWindows()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                    try
                    {
                        if (m_orders.Count > 0 && !m_orders[0].Written)
                        {
                            using (FileStream fs = new FileStream(drive.RootDirectory.FullName + "/PROG.LST", FileMode.OpenOrCreate))
                            {
                                byte[] data = m_orders[0].Prog;
                                fs.Write(data, 0, data.Length);
                                Logger.WriteLine(this, "Файл записан.");
                            }
                        }
                        return true;
                    }
                    catch (IOException e)
                    {
                        Logger.WriteLine(this, e.Message);
                    }
                else
                    Logger.WriteLine(
                        this,
                        "{0}, не является USB-устройством.", // "{0}, type::{1} is not an USB stick.",
                        drive.RootDirectory.FullName/*,
                        drive.DriveType.Description()*/
                    );
            }

            return false;
        }

        private bool WriteToDriveLinux()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            DriveInfo drive = drives[drives.Length - 1];

            if (drive.DriveType == DriveType.Fixed)
                try
                {
                    if (m_orders.Count > 0 && !m_orders[0].Written)
                    {
                        using (FileStream fs = new FileStream(drive.RootDirectory.FullName + "/PROG.LST", FileMode.OpenOrCreate))
                        {
                            byte[] data = m_orders[0].Prog;
                            fs.Write(data, 0, data.Length);
                            Logger.WriteLine(this, "Файл записан.");
                        }
                    }
                    return true;
                }
                catch (IOException e)
                {
                    Logger.WriteLine(this, e.Message);
                }
            else
                Logger.WriteLine(
                    this,
                        "{0}, не является USB-устройством.", // "{0}, type::{1} is not an USB stick.",
                    drive.RootDirectory.FullName/*,
                    drive.DriveType.Description()*/
                );

            return false;
        }

        private string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();

            throw new ApplicationException("Не найден локальный IP-адрес.");
        }

        private void ProcessData(byte[] tarGz)
        {
            string temporaryDir = TemporaryPrefix + unique++.ToString();
            string temporaryInfo = temporaryDir + @"/nfo.txt";
            string temporaryProg = temporaryDir + @"/PROG.LST";

            int id = -1;

            using (var stream = new MemoryStream(tarGz))
            {
                var archive = TarArchive.CreateInputTarArchive(stream);

                try
                {
                    archive.ExtractContents(temporaryDir);
                }
                catch (TarException e)
                {
                    Logger.WriteLine(this, e.Message);
                }

                using (StreamReader sr = new StreamReader(temporaryInfo))
                {
                    id = int.Parse(sr.ReadLine());

                    string notes = sr.ReadLine();
                    string customer = sr.ReadLine();
                    string material = sr.ReadLine();
                    GasType gas = (GasType)int.Parse(sr.ReadLine());
                    string path = sr.ReadLine();

                    int n = int.Parse(sr.ReadLine());

                    for (int i = 0; i < n; ++i)
                    {
                        string[] info = sr.ReadLine().Split(' ', '$');
                    }

                    byte[] prog = File.ReadAllBytes(temporaryProg);

                    m_orders.Add(new Order(
                        id,
                        material,
                        gas,
                        notes,
                        prog
                    ));

                    m_pulled.Add(id);
                    // InternalSetUpdated();
                }
                archive.Close();
                Logger.WriteLine(this, "Получен .TAR от сервера.");
            }
        }

        private void UpdateProgram()
        {
            const string GitHubBaseUrl = @"https://raw.githubusercontent.com/SitadziMado/auto/master/";
            const string VersionFile = @"version.txt";
            const string RemoteDir = @"update/";

            try
            {
                using (var wc = new WebClient())
                {
                    Directory.CreateDirectory(RemoteDir);

                    if (File.Exists(RemoteDir + VersionFile))
                        File.Delete(RemoteDir + VersionFile);

                    wc.DownloadFile(GitHubBaseUrl + VersionFile, RemoteDir + VersionFile);

                    using (var sr = new StreamReader(RemoteDir + VersionFile))
                    {
                        int version = -1;
                        string downloadBase = null;
                        string line;
                        var filesToDownload = new List<string>();

                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Trim();

                            if (line.FirstOrDefault() != '#')
                            {
                                var tokens = (
                                    from v
                                    in line.Split('=')
                                    where v.Length != 0
                                    select v.Trim()
                                ).ToArray();

                                if (tokens.Length == 1)
                                {
                                    filesToDownload.Add(tokens[0]);
                                }
                                else if (tokens.Length == 2)
                                {
                                    switch (tokens[0].ToLower())
                                    {
                                        case "version":
                                            version = int.Parse(tokens[1]);
                                            break;

                                        case "base":
                                            downloadBase = tokens[1];
                                            break;
                                    }
                                }
                                else
                                {
                                    // throw new FormatException("Неверный формат файла настроек.");
                                }
                            }
                        } // while ((line = sr.ReadLine()) != null)

                        int currentVersion = -1;

                        using (var curSr = new StreamReader(VersionFile))
                        {
                            var tokens = (
                                from v
                                in curSr.ReadLine().Split('=')
                                where v.Length != 0
                                select v.Trim().ToLower()
                            ).ToArray();

                            currentVersion = int.Parse(tokens[1]);
                        }

                        if (version != -1 && downloadBase != null && version != currentVersion)
                        {
                            downloadBase = GitHubBaseUrl + downloadBase;

                            foreach (var v in filesToDownload)
                                wc.DownloadFile(downloadBase + v, RemoteDir + v);
                        }
                        else if (version == currentVersion)
                        {
                            // Версии одинаковы.
                        }
                        else
                        {
                            throw new FormatException("Неверный формат файла настроек.");
                        }
                    }
                }

                // Закрыть текущий инстанс, открыть лоадер.
            }
            catch (WebException e)
            {
                Logger.WriteLine(this, "Не удалось соединиться с сервером");
            }
        }
    }
}
