using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    class Program
    {
        #region CALCULATOR
        /*
        static void Main(string[] args)
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            bool isRunning = true;

            while (isRunning)
            {
                Socket clientSocket = listenSocket.Accept();

                while (isRunning)
                {
                    byte[] buffer = new byte[1024];
                    int recvLength = clientSocket.Receive(buffer);
                    if (recvLength <= 0)
                    {
                        isRunning = false;
                    }

                    String msg = Encoding.UTF8.GetString(buffer);

                    string[] tokens = msg.Split(new char[] { '+', '-', '*', '/', '%' });

                    if (tokens.Length > 0 && tokens.Length <= 2)
                    {
                        if (msg.Contains('+'))
                        {
                            msg = (int.Parse(tokens[0]) + int.Parse(tokens[1])).ToString();
                        }
                        else if (msg.Contains('-'))
                        {
                            msg = (int.Parse(tokens[0]) - int.Parse(tokens[1])).ToString();
                        }
                        else if (msg.Contains('*'))
                        {
                            msg = (int.Parse(tokens[0]) * int.Parse(tokens[1])).ToString();
                        }
                        else if (msg.Contains('/'))
                        {
                            msg = (int.Parse(tokens[0]) / int.Parse(tokens[1])).ToString();
                        }
                        else if (msg.Contains('%'))
                        {
                            msg = (int.Parse(tokens[0]) % int.Parse(tokens[1])).ToString();
                        }
                    }



                    Console.WriteLine(msg);
                    buffer = Encoding.UTF8.GetBytes(msg);

                    int sendLength = clientSocket.Send(buffer);
                    if (sendLength <= 0)
                    {
                        isRunning = false;
                    }
                }

                clientSocket.Close();
            }

            listenSocket.Close();
        }
        */
        #endregion

        #region JSON
        /*static void Main(string[] args)
        {
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            bool isRunning = true;

            while (isRunning)
            {
                Socket clientSocket = listenSocket.Accept();

                while (isRunning)
                {
                    byte[] buffer = new byte[1024];
                    int recvLength = clientSocket.Receive(buffer);
                    if (recvLength <= 0)
                    {
                        isRunning = false;
                    }

                    String msg = Encoding.UTF8.GetString(buffer);
                    JObject recvJObject = JsonConvert.DeserializeObject<JObject>(msg);

                    Console.WriteLine(recvJObject["message"]);

                    JObject jObject = new JObject(); ;
                    jObject.Add("message", "반가워요");
                    string jsonData = JsonConvert.SerializeObject(jObject);
                    buffer = Encoding.UTF8.GetBytes(jsonData);

                    int sendLength = clientSocket.Send(buffer);
                    if (sendLength <= 0)
                    {
                        isRunning = false;
                    }
                }

                clientSocket.Close();
            }

            listenSocket.Close();
        }*/
        #endregion

        #region IMAGE
        static void Main(string[] args)
        {
            int bufferSize = 1024;

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);
            listenSocket.Bind(listenEndPoint);
            listenSocket.Listen(10);

            if (!File.Exists("image.jpg"))
            {
                Console.WriteLine("이미지 X");
                return;
            }

            byte[] fileData = File.ReadAllBytes("image.jpg");
            int fileSize = fileData.Length;
            int currentSize = 0;

            bool isRunning = true;
            bool isFileSending = true;

            while (isRunning)
            {
                Socket clientSocket = listenSocket.Accept();
                Console.WriteLine($"전송 시작 {fileSize} 바이트");
                //사이즈 전송
                byte[] buffer = BitConverter.GetBytes(fileSize);

                int sendLength = clientSocket.Send(buffer);
                if (sendLength <= 0)
                {
                    isRunning = false;
                }

                //파일 전송
                while (isFileSending)
                {
                    buffer = new byte[bufferSize];

                    //종료 위치를 먼저 계산
                    int endLength = bufferSize;
                    if (fileSize < currentSize + bufferSize)
                    {
                        endLength = fileSize % bufferSize;
                        isFileSending = false;
                    }

                    //buffer에 데이터 복사
                    for (int i = 0; i < endLength; i++)
                    {
                        buffer[i] = fileData[currentSize + i];
                    }

                    currentSize += endLength;

                    //전송
                    sendLength = clientSocket.Send(buffer);
                    if (sendLength <= 0)
                    {
                        isRunning = false;
                    }
                }
                clientSocket.Close();

                Console.WriteLine("전송 종료");
            }

            listenSocket.Close();
        }
        #endregion
    }
}
