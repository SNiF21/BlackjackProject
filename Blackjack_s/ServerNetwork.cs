using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Blackjack_s
{
    public class ServerNetwork
    {
        private TcpListener listener;
        private Socket clientSocket;
        private StreamReader reader;
        private StreamWriter writer;
        private Thread listenThread;

        public event Action<string> CommandReceived;

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, 3000);
            listener.Start();

            listenThread = new Thread(WaitForClient);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void WaitForClient()
        {
            clientSocket = listener.AcceptSocket();

            NetworkStream stream = new NetworkStream(clientSocket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            Listen();
        }

        private void Listen()
        {
            while (true)
            {
                string command = reader.ReadLine();
                if (command == null)
                    break;

                CommandReceived?.Invoke(command);
            }
        }

        public void Send(string message)
        {
            writer?.WriteLine(message);
        }
    }
}
