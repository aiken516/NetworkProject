using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPotin = new IPEndPoint(IPAddress.Loopback, 4000);
            serverSocket.Connect(serverEndPotin);

            bool isRunning = true;

            while (isRunning)
            {
                byte[] buffer = new byte[1024];

                String msg = Console.ReadLine();
                //buffer = Encoding.UTF8.GetBytes(msg);
                JObject jObject = new JObject(); ;
                jObject.Add("message", "안녕하세요");
                string jsonData = JsonConvert.SerializeObject(jObject);
                buffer = Encoding.UTF8.GetBytes(jsonData);

                int sendLength = serverSocket.Send(buffer);
                if (sendLength <= 0)
                {
                    isRunning = false;
                }

                byte[] buffer2 = new byte[1024];

                int recvLength = serverSocket.Receive(buffer2);
                if (recvLength <= 0)
                {
                    isRunning = false;
                }

                JObject recvJObject = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(buffer2));

                Console.WriteLine(recvJObject["message"]);
            }

            serverSocket.Close();
        }
    }
}
