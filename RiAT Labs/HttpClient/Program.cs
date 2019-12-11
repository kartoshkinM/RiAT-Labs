using System;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://213.226.112.78";
            Console.WriteLine($"Url для подключения: {url}\nВведите порт (если нужен)");
            var port = Console.ReadLine();
            var httpClient = new Client(url, port);
            Console.WriteLine(httpClient.Ping());
            //return;
            var inputData = httpClient.getInputData();
            Console.WriteLine(inputData);
            var outputData = Serializer.Actions.DataTransform("json", inputData);
            httpClient.writeAnswer(outputData);

            Console.ReadKey();
        }
    }
}
