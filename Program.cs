using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPUDPAssignment;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Start TCP Server");
            Console.WriteLine("2. Start UDP Server");
            Console.WriteLine("3. Send TCP Message");
            Console.WriteLine("4. Send UDP Message");
            Console.WriteLine("5. Exit");

            Console.Write("\nYour answer: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    TcpServer.Start();
                    break;
                case "2":
                    UdpServer.Start();
                    break;
                case "3":
                    Console.Write("Enter the message to send via TCP: ");
                    string messageTCP = Console.ReadLine();
                    Client.SendTcpMessage(messageTCP);
                    break;
                case "4":
                    Console.Write("Enter the message to send via UDP: ");
                    string messageUDP = Console.ReadLine();
                    Client.SendUdpMessage(messageUDP);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    break;
            }

            Thread.Sleep(new Random().Next(3000, 5000));
        }
    }

    public class TcpServer
    {
        public static void Start()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, Settings.TcpPort);
            tcpListener.Start();
            Console.WriteLine("TCP Server initiated. Waiting for messages...");

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Console.WriteLine("TCP Client connected.");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("TCP Message received: " + message);

                stream.Close();
                client.Close();
            }
        }
    }

    public class UdpServer
    {
        public static void Start()
        {
            UdpClient udpListener = new UdpClient(Settings.UdpPort);
            Console.WriteLine("UDP Server initiated. Waiting for messages...");

            while (true)
            {
                IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Any, Settings.UdpPort);
                byte[] buffer = udpListener.Receive(ref clientEndpoint);
                string message = Encoding.ASCII.GetString(buffer);
                Console.WriteLine("UDP Message received: " + message);
            }

        }
    }

    public class Client
    {
        public static void SendTcpMessage(string message)
        {

            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Loopback, Settings.TcpPort);
            NetworkStream stream = tcpClient.GetStream();

            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("TCP Message sent: " + message);

            stream.Close();
            tcpClient.Close();
        }

        public static void SendUdpMessage(string message)
        {

            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, Settings.UdpPort);

            byte[] data = Encoding.ASCII.GetBytes(message);
            udpClient.Send(data, data.Length, serverEndPoint);
            Console.WriteLine("\nUDP Message sent: " + message);

            udpClient.Close();
        }
    }

    public class Settings
    {
        public const int TcpPort = 4568;
        public const int UdpPort = 1239;

    }

}


