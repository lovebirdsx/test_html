using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleWebServer {
    public class SimpleWebServer {
        Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        bool isEndService = false;

        public void Start() {
            var port = 8000;
            listenSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            listenSocket.Listen();
            Console.WriteLine($"Server start at {port}");
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SimpleWebServer");
            Console.WriteLine($"Dir: {dir}");
            Run();
        }

        void Run() {
            while (!isEndService) {
                var socket = listenSocket.Accept();
                Console.WriteLine($"Accept: {socket.RemoteEndPoint}");
                var data = new byte[1024 * 1024 * 2];
                var length = socket.Receive(data);
                if (length > 0) {
                    var requestText = Encoding.Default.GetString(data, 0, length);
                    var context = new HttpContext(requestText);
                    var req = context.Request;
                    Console.WriteLine($"{req.HttpMethod} {req.Url}");

                    var application = new HttpApplication();
                    application.ProcessRequest(context);
                    socket.Send(context.Response.GetResponseHeader());
                    if (context.Response.Body != null) {
                        socket.Send(context.Response.Body);
                    }
                } else {
                    Console.WriteLine($"No data from {socket.RemoteEndPoint}");
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}