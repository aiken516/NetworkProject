using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        #region
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
        }
    }
}
