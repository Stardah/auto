using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace Automation2010
{

    public class Client
    {
        public Client(TcpClient client, Form1 form)
        {
            var remoteEP = (client.Client.RemoteEndPoint as IPEndPoint);
            remoteEP.Port = 22801; /// ToDo: надо фильтровать по порту, я думаю       

            List<string> parameters = new List<string>();
            byte[] Buffer_in = new byte[1024];
            byte[] Buffer_out;
            // Uses the GetStream public method to return the NetworkStream.
            NetworkStream netStream = client.GetStream();

            // Читаем из потока клиента до тех пор, пока от него поступают данные
            try
            {
                if (netStream.CanRead)
                {
                    byte[] bytes = new byte[client.ReceiveBufferSize];

                    // Read can return anything from 0 to numBytesToRead. 
                    // This method blocks until at least one byte is read.
                    netStream.Read(bytes, 0, client.ReceiveBufferSize);

                    // Returns the data received from the host to the console.
                    string returndata = Encoding.ASCII.GetString(bytes);
                    //Request += Encoding.ASCII.GetString(Buffer_in, 0, Count);
                    int i = 0; // Парсим
                    foreach (string s in returndata.Split('$'))
                    {
                        parameters.Add(s);
                        ++i;
                    }
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show("Херня" + e);
            }
            // Отправка tar архива 
            if (parameters[0] == "get_tar") // get_tar id
            {
                string FilePath = parameters[1];
                byte[] dataToSend = File.ReadAllBytes(FilePath);
                netStream.Write(dataToSend, 0, dataToSend.Length);
            }
            // Выдача списка IDшников
            else if (parameters[0] == "get_orders") // get_orders pi_num
            {
                bool flag = false;
                StringBuilder sb = new StringBuilder();
                if (parameters[1] == "1")
                    foreach (Order order in form.orders)
                    {
                        if (order.machine == "ближний" && order.status == 1)
                        {
                            sb.Append(order.id.ToString() + "$");
                            flag = true;
                        }
                    }
                else foreach (Order order in form.orders)
                        if (order.machine == "дальний" && order.status == 1)
                        {
                            sb.Append(order.id.ToString() + "$");
                            flag = true;
                        }
                Buffer_out = Encoding.ASCII.GetBytes(sb.ToString()); // Приведем строку к виду массива байт
                if (flag) netStream.Write(Buffer_out, 0, Buffer_out.Length);
                //MessageBox.Show(flag.ToString(),"Get_Orders Result");
            }
            // Окончание заказа (получение результатов работы над заказом)
            else if (parameters[0] == "end_order") // end_order id num remained cell
            {
                int id = Int32.Parse(parameters[1]);
                // Читаем ответ от пишки
                form.orders[id - form.nextID + 1].status = 3;
                form.ifupdate = true;
            }
            else
            {
                MessageBox.Show("Ошибка");
                // 404
                string Str = "error";
                // Приведем строку к виду массива байт
                Buffer_out = Encoding.ASCII.GetBytes(Str);
                // Отправим его клиенту
                netStream.Write(Buffer_out, 0, Buffer_out.Length);
            }
            // Закроем соединение
            netStream.Flush();
            client.Close();
        }
    }

}
