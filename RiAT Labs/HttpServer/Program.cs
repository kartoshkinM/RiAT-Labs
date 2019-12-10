using System;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер порта");
            Server server = new Server(Console.ReadLine());
            server.Start();
        }
    }
}
