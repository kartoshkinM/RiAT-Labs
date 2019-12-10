using System;
using System.Net;
using System.Text;

namespace HttpClient
{
    public class Client
    {
        private readonly string Url;
        private readonly WebClient _WebClient;

        public Client(string url, string port)
        {
            _WebClient = new WebClient();
            Url = port != "" ? $"{url}:{port}" : $"{url}";
        }

        public string Ping()
        {
            try
            {
                Console.WriteLine($"Connecting to {Url}");
                Console.Write("Ping...");
                byte[] inputData = _WebClient.DownloadData(Url + "/Ping");
                string inputDataStr = Encoding.UTF8.GetString(inputData);
                Console.Write("Pong!");
                return inputDataStr;
            }
            catch (WebException)
            {
                throw;
            }
        }

        public string getInputData()
        {
            byte[] inputData = _WebClient.DownloadData(Url + "/GetInputData");
            string inputDataStr = Encoding.UTF8.GetString(inputData);
            return inputDataStr;
        }

        public string writeAnswer(string answer)
        {
            byte[] outputData = Encoding.UTF8.GetBytes(answer);
            var feedback = _WebClient.UploadData(Url + "/WriteAnswer", outputData);
            var feedbackStr = Encoding.UTF8.GetString(feedback);
            return feedbackStr;
        }

    }
}
