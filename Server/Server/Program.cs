using System;
using System.Text;
 
using System.Net;
using System.Net.Sockets;
 
namespace TcpServer
{
    class Program
    {
        private const int BUFFER_SIZE = 1024;
 
        static void Main(string[] args)
        {
            Console.Title = "TCP/IP Echo Server";
 
            if (args.Length > 1)
                throw new ArgumentException("Parameters: [Port]");
 
            int port = (args.Length == 1) ? Int32.Parse(args[0]) : 5000;
 
            TcpListener listener = null;
 
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
            }
            catch (SocketException e)
            {
                Console.WriteLine("{0}: {1}", e.ErrorCode, e.Message);
                Environment.Exit(e.ErrorCode);
            }
 
            var buffer = new byte[BUFFER_SIZE];
 
            while (true)
            {
                TcpClient client = null;
                NetworkStream stream = null;
 
                try
                {
                    client = listener.AcceptTcpClient();
                    stream = client.GetStream();
                    var totalBytesReceived = 0;
 
                    Console.WriteLine("Handling client:");
 
                    while (true)
                    {
                        var bytesReceived = stream.Read(buffer, 0, BUFFER_SIZE);
                        Console.WriteLine(
                            Encoding.ASCII.GetString(buffer, totalBytesReceived, bytesReceived)
                            );
 
                        stream.Write(buffer, 0, buffer.Length);
                        totalBytesReceived += bytesReceived;
                        if (bytesReceived != BUFFER_SIZE) break;
 
                    }
 
                    Console.WriteLine("Echoed a {0} bytes length message.n", totalBytesReceived);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    stream.Close();
                    client.Close();
                }
            }
        }
    }
}