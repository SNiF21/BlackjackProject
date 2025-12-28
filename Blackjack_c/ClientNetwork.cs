using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Blackjack_c
{
    public class ClientNetwork
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;

        public event Action<string> MessageReceived;

        public void Connect(string ip = "127.0.0.1", int port = 3000)
        {
            client = new TcpClient(ip, port);

            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            listenThread = new Thread(Listen);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void Listen()
        {
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;

                MessageReceived?.Invoke(line);
            }
        }

        public void Send(string message)
        {
            writer.WriteLine(message);
        }
    }
}
