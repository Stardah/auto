using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Tar;
using HtmlAgilityPack;

namespace Automation2010
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public string ip = "0";
        public List<Order> orders = new List<Order>();
        public Server server;
        public int nextID; // Текущий, последний ID 
        private Sorter columnSorter; // Сортировочки
        public bool ifupdate = false;

        int curId = -1;

        public Form1()
        {
            InitializeComponent();
            CreateListViews();
            //CreateLog();
            CreateLog();
            // Create an instance of a ListView column sorter and assign it // to the ListView control.
            columnSorter = new Sorter();
            this.listAll.ListViewItemSorter = columnSorter;
            this.listDone.ListViewItemSorter = columnSorter;
            this.listFirst.ListViewItemSorter = columnSorter;
            this.listSecond.ListViewItemSorter = columnSorter;
            //setTimer(true); // delete this
            server = new Server(22800, this);
        }

        private string[] ShowDialog(string name)
        {
            string[] info = new string[3];
            info[0] = "none";
            info[1] = "1";
            info[2] = "none";
            // Create and display an instance of the dialog box.
            using (Dialog dlg = new Dialog())
            {
                dlg.Text = name;
                dlg.ShowDialog();
                // Determine the state of the DialogResult property for the form.
                if (dlg.DialogResult == DialogResult.OK)
                {
                    // Display the state that was selected in the dialog box's combo 
                    // box in a MessageBox.
                    info[0] = dlg.GetMaterial;
                    info[1] = dlg.GetGas;
                    info[2] = dlg.GetAbout;
                    return info;
                }
            }
            return info;
        }

        public void ConnectionStatus(bool flag)
        {
            if (flag)
            {
                connection_status.ForeColor = Color.Green;
                connection_status.Text = "Соединено";
            }
            else
            {
                connection_status.ForeColor = Color.Red;
                connection_status.Text = "Нет сигнала";
            }
        }

        private void CreateLog()
        {
            try
            {
                using (StreamReader file = new StreamReader("log.txt"))
                {
                    nextID = Int32.Parse(file.ReadLine());
                    file.Close();
                }
            }
            catch (Exception e)
            {
                using (StreamWriter file = new StreamWriter("log.txt"))
                {
                    file.WriteLine("0");
                    file.Close();
                }
                nextID = 0;
            }

        }

        private void WriteLog()
        {
            using (StreamWriter file = new StreamWriter("log.txt", false))
            {
                file.WriteLine(nextID.ToString());
                file.Close();
            }

        }

        private void ReadLog()
        {
            using (StreamReader file = new StreamReader("log.txt"))
            {
                nextID = Int32.Parse(file.ReadLine());
                file.Close();
            }
            //nextID = 0;
        }

        private void CreateTar(string path, int id) // Пакует в архив заказ по имени архива, пути до папки файлов и id
        {
            using (var outStream = File.Create(nextID.ToString()))
            //using (var gzoStream = new GZipOutputStream(outStream))
            using (var tarArchive = TarArchive.CreateOutputTarArchive(outStream))
            {
                string s = "nope";
                try
                {
                    tarArchive.RootPath = Path.GetDirectoryName("info.txt"); // Пакуем info
                    var tarEntry = TarEntry.CreateEntryFromFile("info.txt");
                    tarEntry.Name = Path.GetFileName("info.txt");
                    tarArchive.WriteEntry(tarEntry, true);
                    s = "+info";

                    tarArchive.RootPath = Path.GetDirectoryName(@"C:\users\masch01\" + orders[id].name + ".LST"); // Пакуем LST #TODO
                    tarEntry = TarEntry.CreateEntryFromFile(@"C:\users\masch01\" + orders[id].name + ".LST");
                    tarEntry.Name = Path.GetFileName("PROG.LST");
                    tarArchive.WriteEntry(tarEntry, true);
                    s = "done";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), s);
                }
            }
            nextID++;
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            // TO DO: connection to Pi1
            if (curId != -1)
            {
                orders[curId].machine = "ближний";
                orders[curId].status = 1;
                orders[curId].date = DateTime.Now.ToString();
                curId = -1;
                UpdateLists();
            }
            else MessageBox.Show("Ни одна УП не выбрана, пожалуйста, выберите УП!", "Действие отменено!");
        }

        private void btn_second_Click(object sender, EventArgs e)
        {
            // TO DO: connection to Pi2
            if (curId != -1)
            {
                orders[curId].machine = "дальний";
                orders[curId].status = 1;
                orders[curId].date = DateTime.Now.ToString();
                curId = -1;
                UpdateLists();           // Обновляем списоки
            }
            else MessageBox.Show("Ни одна УП не выбрана, пожалуйста, выберите УП!", "Действие отменено!");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (curId != -1)
            {
                orders.RemoveAt(curId);
                curId = -1;
                UpdateLists();
                nextID--;
            }
            else MessageBox.Show("Ни одна УП не выбрана, пожалуйста, выберите УП!", "Действие отменено!");
        }

        private void CreateListViews()
        {
            // Create a new listAll

            listAll.View = View.Details; // Set the view to show details.
            //listView1.LabelEdit = true; // Allow the user to edit item text.
            listAll.AllowColumnReorder = true; // Allow the user to rearrange columns.
            //listView1.CheckBoxes = true; // Display check boxes.
            listAll.FullRowSelect = true; // Select the item and subitems when selection is made.
            listAll.GridLines = true; // Display grid lines.
            listAll.Sorting = SortOrder.Ascending; // Sort the items in the list in ascending order.

            // Create columns for the items and subitems.
            listAll.Columns.Add("Название", 120, HorizontalAlignment.Left);
            listAll.Columns.Add("Материал", 100, HorizontalAlignment.Left);
            listAll.Columns.Add("Газ", 60, HorizontalAlignment.Left);
            listAll.Columns.Add("Дата", 150, HorizontalAlignment.Center);
            listAll.Columns.Add("Примечание", -2, HorizontalAlignment.Left);

            // Create two ImageList objects.
            ImageList imageListSmall = new ImageList();
            ImageList imageListLarge = new ImageList();

            // Initialize the ImageList objects with bitmaps.
            imageListSmall.Images.Add(Bitmap.FromFile("icon_gray.bmp")); // 0
            imageListSmall.Images.Add(Bitmap.FromFile("icon_yellow.bmp")); // 1
            imageListSmall.Images.Add(Bitmap.FromFile("icon_red.bmp")); // 2
            imageListSmall.Images.Add(Bitmap.FromFile("icon_green.bmp"));// 3
            imageListLarge.Images.Add(Bitmap.FromFile("icon_gray.bmp"));
            imageListLarge.Images.Add(Bitmap.FromFile("icon_yellow.bmp"));
            imageListLarge.Images.Add(Bitmap.FromFile("icon_red.bmp"));
            imageListLarge.Images.Add(Bitmap.FromFile("icon_green.bmp"));

            //Assign the ImageList objects to the ListView.
            listAll.LargeImageList = imageListLarge;
            listAll.SmallImageList = imageListSmall;

            // Create listFirst ____________________________

            listFirst.View = View.Details; // Set the view to show details.
            listFirst.AllowColumnReorder = true; // Allow the user to rearrange columns.
            listFirst.FullRowSelect = true; // Select the item and subitems when selection is made.
            listFirst.GridLines = true; // Display grid lines.
            listFirst.Sorting = SortOrder.Ascending; // Sort the items in the list in ascending order.

            listFirst.LargeImageList = imageListLarge;
            listFirst.SmallImageList = imageListSmall;

            listFirst.Columns.Add("Название", 100, HorizontalAlignment.Center);
            //listFirst.Columns.Add("Детали", 60, HorizontalAlignment.Center);
            listFirst.Columns.Add("Дата", -2, HorizontalAlignment.Center);

            // Create listSecond _____________________________________________________


            listSecond.View = View.Details; // Set the view to show details.
            listSecond.AllowColumnReorder = true; // Allow the user to rearrange columns.
            listSecond.FullRowSelect = true; // Select the item and subitems when selection is made.
            listSecond.GridLines = true; // Display grid lines.
            listSecond.Sorting = SortOrder.Ascending; // Sort the items in the list in ascending order.

            listSecond.LargeImageList = imageListLarge;
            listSecond.SmallImageList = imageListSmall;

            listSecond.Columns.Add("Название", 100, HorizontalAlignment.Center);
            //listSecond.Columns.Add("Детали", 60, HorizontalAlignment.Center);
            listSecond.Columns.Add("Дата", -2, HorizontalAlignment.Center);


            // Create listDone _____________________________________________________

            listDone.View = View.Details; // Set the view to show details.
            listDone.AllowColumnReorder = true; // Allow the user to rearrange columns.
            listDone.FullRowSelect = true; // Select the item and subitems when selection is made.
            listDone.GridLines = true; // Display grid lines.
            listDone.Sorting = SortOrder.Ascending; // Sort the items in the list in ascending order.

            listDone.LargeImageList = imageListLarge;
            listDone.SmallImageList = imageListSmall;

            // Create columns for the items and subitems.
            listDone.Columns.Add("Название", 100, HorizontalAlignment.Center);
            listDone.Columns.Add("Дата", -2, HorizontalAlignment.Center);
        }

        public void AddOrderToLV(ListView list, string name, string amount, string data, int status)
        {
            ListViewItem item = new ListViewItem(name, status);
            if (amount != null) item.SubItems.Add(amount);
            if (data != null) item.SubItems.Add(data);
            list.Items.Add(item);
        }

        public void FillListAll(ListView list, string name, string material, string gas, string data, string about)
        {
            ListViewItem item = new ListViewItem(name, 0);
            if (material != null) item.SubItems.Add(material);
            if (gas != null) item.SubItems.Add(gas);
            if (data != null) item.SubItems.Add(data);
            if (about != null) item.SubItems.Add(about);
            list.Items.Add(item);
        }

        public void AddFiles(string[] folders)
        {
            foreach (string dir in folders) // Новый заказ
            {
                Table newTable = new Table();
                try
                {
                    // Read html 
                    newTable = Parser.ParseHtml(dir);

                    if (newTable == null) throw new NullReferenceException();
                    string filename = @"C:\users\masch01\" + newTable.name + ".LST";
                    if (!File.Exists(filename)) throw new NullReferenceException(); 

                    string[] info = new string[3];

                    info = ShowDialog(newTable.name); // 0 - материал, 1 - газ, 2 - подробности


                    // Заполняем данные
                    orders.Add(new Order(
                        nextID,
                        0,
                        newTable.name,
                        newTable.lstPath,
                        newTable.num.Count.ToString(),
                        "",
                         "",
                         info[0],
                         info[1],
                         info[2]
                            )
                            );

                    int i = dir.Length - 1;
                    char ch = '0';
                    // Удаляем всё до первого имени
                    while (ch != '\\')
                    {
                        ch = dir[i];
                        i--;
                    }
                    // Записываем инфу таблицы в файл info
                    using (StreamWriter file = new StreamWriter("info.txt"))
                    {
                        file.WriteLine(nextID.ToString());
                        file.WriteLine(info[2]); // about
                        file.WriteLine(newTable.name);
                        file.WriteLine(info[0]); // material
                        file.WriteLine(info[1]); // gas
                        file.WriteLine(newTable.lstPath);
                        file.WriteLine("0");
                        for (int c = 0; c < newTable.num.Count; c++)
                            file.WriteLine(newTable.num[c] + "$" + newTable.amount[c] + "$" + "xui");
                    }
                    WriteLog();
                    CreateTar(dir.Remove(i + 2), orders.Count - 1); // Пакуем всё в архив
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Не удалось считать файл!", "Действие отменено");
                }
                UpdateLists(); // Отображем список
            }

        }

        // Отрисовка таблицы заказов
        public void UpdateLists()
        {
            listAll.Items.Clear();
            listDone.Items.Clear();
            listFirst.Items.Clear();
            listSecond.Items.Clear();
            for (int j = 0; j < orders.Count; j++)
            {
                // Новые заказы
                if (orders[j].status == 0) FillListAll(listAll, orders[j].name, orders[j].material, orders[j].gas, orders[j].date, orders[j].about);
                // Завершённые
                if (orders[j].status == 3 || orders[j].status == 2) AddOrderToLV(listDone, orders[j].name, null, orders[j].date, orders[j].status);
                // В очереди на ближний станок
                if (orders[j].status == 1 && orders[j].machine == "ближний") AddOrderToLV(listFirst, orders[j].name, null, orders[j].date, orders[j].status);
                // В очереди на дальний станок
                if (orders[j].status == 1 && orders[j].machine == "дальний") AddOrderToLV(listSecond, orders[j].name, null, orders[j].date, orders[j].status);
            }
        }

        // Показать детали выбранного заказа
        public void ShowOrder(int id)
        {
            // curId = id;
        }

        // Клик по основному списку
        private void listAll_Click(object sender, EventArgs e)
        {
            curId = -1;
            if (orders.Count > 0)
                for (int i = 0; i < orders.Count; i++)
                    if (orders[i].date == listAll.Items[listAll.FocusedItem.Index].SubItems[3].Text)
                    {
                        curId = i;
                        break;
                    }
            btnNear.Enabled = true;
            btnFurther.Enabled = true;
        }

        // Клик по основному списку
        private void listDone_Click(object sender, EventArgs e)
        {
            curId = -1;
            try
            {
                btnNear.Enabled = false;
                btnFurther.Enabled = false;
            }
            catch (NullReferenceException)
            {

            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        public void Connect()
        {
            ip = textIP.Text;
            if (textIP.Text == "") ip = "0";
            server.Connection();
        }

        private void listFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (orders.Count > 0)
                for (int i = 0; i < orders.Count; i++)
                    if (orders[i].date == listFirst.Items[listFirst.FocusedItem.Index].SubItems[1].Text)
                    {
                        ShowOrder(i);
                        break;
                    }
            curId = -1;
            //btnNear.Enabled = false;
            //btnFurther.Enabled = false;
        }

        private void listSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (orders.Count > 0)
                for (int i = 0; i < orders.Count; i++)
                    if (orders[i].date == listSecond.Items[listSecond.FocusedItem.Index].SubItems[1].Text)
                    {
                        ShowOrder(i);
                        break;
                    }
            curId = -1;
            //btnNear.Enabled = false;
            //btnFurther.Enabled = false;
        }

        private void listAll_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //foreach (string file in files)
            AddFiles(files);
        }

        private void listAll_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void listAll_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.if ( e.Column == lvwColumnSorter.SortColumn )
            if (e.Column == columnSorter.SortColumn)
            {

                // Reverse the current sort direction for this column.
                if (columnSorter.Order == SortOrder.Ascending)
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                columnSorter.SortColumn = e.Column;
                columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listAll.Sort();
        }

        private void listFirst_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.if ( e.Column == lvwColumnSorter.SortColumn )
            if (e.Column == columnSorter.SortColumn)
            {

                // Reverse the current sort direction for this column.
                if (columnSorter.Order == SortOrder.Ascending)
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                columnSorter.SortColumn = e.Column;
                columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listFirst.Sort();
        }

        private void listSecond_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.if ( e.Column == lvwColumnSorter.SortColumn )
            if (e.Column == columnSorter.SortColumn)
            {

                // Reverse the current sort direction for this column.
                if (columnSorter.Order == SortOrder.Ascending)
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                columnSorter.SortColumn = e.Column;
                columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listSecond.Sort();
        }

        private void listDone_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.if ( e.Column == lvwColumnSorter.SortColumn )
            if (e.Column == columnSorter.SortColumn)
            {

                // Reverse the current sort direction for this column.
                if (columnSorter.Order == SortOrder.Ascending)
                {
                    columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                columnSorter.SortColumn = e.Column;
                columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listDone.Sort();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (ifupdate)
            {
                UpdateLists();
                ifupdate = false;
            }
        }

        private void textIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Connect();
                e.Handled = true;
            }

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back || textIP.Text.ToString().Length > 2 && e.KeyChar != (char)Keys.Back)
                e.Handled = true;

        }

        private void listAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            curId = -1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteLog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    // Класс заказа
    public class Order
    {
        public int id;
        public string path;
        public int status;
        public string name;
        public string amount;
        public string machine;
        public string date;
        public string material;
        public string gas;
        public string about;

        public Order(int id, int status, string name, string path, string amount, string machine, string date, string material, string gas, string about)
        {
            this.path = path;
            this.status = status;
            this.name = name;
            this.amount = amount;
            this.machine = machine;
            this.date = date;
            this.id = id;
            this.material = material;
            this.gas = gas;
            this.about = about;
        }

    }

    // Класс данных таблицы
    public class Table
    {
        public string name;
        public string lstPath;
        public List<string> num;
        public List<string> amount;
    }

    // Инфа о компьютерах сети
    public class Brothers
    {
        public string ip;
        public string hostname;
    }
}
