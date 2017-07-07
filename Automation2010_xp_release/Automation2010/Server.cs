using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Automation2010
{
    // Сервер, получающий данные, вызывающий клиенты - обработчики
    public class OldServer
    {

        string output = "";
        Form1 form;
        Brothers[] brothers;

        TcpListener Listener = null; // Объект, принимающий TCP-клиентов

        // Запуск сервера
        public OldServer(int Port, Form1 form)
        {
            this.form = form;
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            try
            {
                // Создаем "слушателя" для указанного порта
                Listener = new TcpListener(IPAddress.Any, 22800);
                Listener.Start(10); // Запускаем его
            }
            catch (Exception e)
            {
                output = "Error: " + e.ToString();
                // MessageBox.Show(output);
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    // Принимаем новых клиентов
                    TcpClient Client = Listener.AcceptTcpClient();
                    // Создаем поток
                    Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                    // И запускаем этот поток, передавая ему принятого клиента
                    Thread.Start(Client);

                    Delay(5000);
                }
            });
        }

        public static Task Delay(double milliseconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) =>
            {
                tcs.TrySetResult(true);
            };
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }

        public int GetAllIPAndHostNames()
        {
            string strHostName;

            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();

            IPHostEntry remoteIP;

            // Using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            int i = 0;

            brothers = new Brothers[addr.Length];

            while (i < addr.Length)
            {
                // IP
                brothers[i] = new Brothers();
                brothers[i].ip = addr[i].ToString();
                // HostName
                try
                {
                    remoteIP = Dns.GetHostEntry((addr[i]));
                    brothers[i].hostname = remoteIP.HostName;
                }
                catch (SocketException)
                {
                    MessageBox.Show(i.ToString(), "");
                }
                i++;
            }
            return 0;
        }

        public void Connection()
        {
            GetAllIPAndHostNames();
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            brothers[0].ip = "192.168.0." + form.ip; //"192.168.137."+form.ip 127.0.0.1;
            /*brothers = new Brothers[256];
            for (int i = 0; i < 256; i++)
            {
                brothers[i] = new Brothers();
                brothers[i].ip = "192.168.0." + i.ToString();
            }*/
            for (int i = 0; i < 1; i++) //brothers.Length
            {
                IPAddress ipAdd = IPAddress.Parse(brothers[i].ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAdd, 22801);
                try
                {
                    soc.Connect(remoteEP);
                    // Send
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes("Hello");
                    soc.Send(byData);
                    soc.Shutdown(SocketShutdown.Both);
                    soc.Close();
                    form.ConnectionStatus(true);

                }
                catch (Exception e)
                {
                    MessageBox.Show(brothers[i].ip + e);
                }
                finally
                {

                }
            }
        }

        // Остановка сервера
        ~OldServer()
        {
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }

        public void ClientThread(Object StateInfo)
        {
            new OldClient((TcpClient)StateInfo, form);
        }

    }

}
