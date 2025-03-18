using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        /*static void Main(string[] args)
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
        }*/

        static void Main(string[] args)
        {
            int bufferSize = 1024;
            int currentSize = 0;

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint serverEndPotin = new IPEndPoint(IPAddress.Loopback, 4000);
            serverSocket.Connect(serverEndPotin);

            //사이즈 먼저 받아오기
            byte[] buffer = new byte[sizeof(int)];

            int recvLength = serverSocket.Receive(buffer);
            int fileSize = BitConverter.ToInt32(buffer, 0);

            //파일 담을 버퍼 생성
            byte[] fileBuffer = new byte[fileSize];


            bool isRunning = true;

            while (isRunning)
            {
                buffer = new byte[bufferSize];

                recvLength = serverSocket.Receive(buffer);
                if (recvLength <= 0)
                {
                    isRunning = false;
                }

                int endLength = bufferSize;
                if (fileSize < currentSize + bufferSize)
                {
                    endLength = fileSize % bufferSize;
                    isRunning = false;
                }

                for (int i = 0; i < endLength; i++)
                {
                    fileBuffer[currentSize + i] = buffer[i];
                }

                currentSize += bufferSize;
            }

            File.WriteAllBytes("image.jpg", fileBuffer);

            serverSocket.Close();
        }
    }
}
