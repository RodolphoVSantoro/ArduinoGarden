using System;
using System.Text;
using System.Net.Sockets;
namespace TcpEchoClient 
{
    public class Soquete
    {
        private const int BUFFER_SIZE = 1024;
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer;
        private int totalBytesReceived;
        
        public Soquete(String IP, int porta)
        {
            try
            {
                this.client = new TcpClient(IP, porta);
                this.stream = client.GetStream();
                this.buffer = new byte[BUFFER_SIZE];
                this.totalBytesReceived = 0;
            }
            catch (Exception e)
            {
                System.Environment.Exit(1);
            }
        }
        
        public String listen()
        {
            String retorno="";
            try
            {
                totalBytesReceived = 0;

                while (true)
                {
                    var bytesReceived = stream.Read(this.buffer, 0, BUFFER_SIZE);
                    retorno = Encoding.UTF8.GetString(this.buffer, totalBytesReceived, bytesReceived);
                    totalBytesReceived += bytesReceived;
                    if (bytesReceived != BUFFER_SIZE) break;
                }
            }
            catch (Exception e)
            {
                System.Environment.Exit(1);
            }
            return retorno;
        }
        
        public void enviar(String msg)
        {
            try
            {
                Console.Title = "TCP Echo Client";
                var bytesToSend = Encoding.UTF8.GetBytes(msg);
                Console.WriteLine("Enviado {0}({1} Bytes)", msg, bytesToSend.Length);
                stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception e)
            {
                System.Environment.Exit(1);
            }
        }
    }

    class Program 
    {
        
        static void Main(string[] args) 
        {
            Soquete socket = new Soquete("127.0.0.1", 5000);
            String msg;
            while (true) 
            {
                Console.WriteLine(socket.listen());
                msg = Console.ReadLine();
                socket.enviar(msg);
                Console.WriteLine(socket.listen());
            } 
        } 
    } 
}