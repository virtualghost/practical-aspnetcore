using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpEcho
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            int port = 8087;
            Console.WriteLine($"Connecting to port {port}");
         
            var clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, port));
            var stream = new NetworkStream(clientSocket);

            Console.Write("Ready for your input: ");
            var input = Console.ReadLine();
            while(input != "")
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(input);
                await stream.WriteAsync(data, 0, data.Length);

                data = new Byte[256];
                // Read the Tcp Server Response Bytes.
                int bytes = stream.Read(data, 0, data.Length);
                var response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Received: {0}", response);
                Console.Write("Ready for your input: ");
                input = Console.ReadLine();
            }

            clientSocket.Disconnect(reuseSocket: true);
        }
    }
}