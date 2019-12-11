using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Serializer;

namespace HttpServer
{
    class Server
    {
        readonly string Url;
        readonly HttpListener httpListener;
        readonly ISerialize serializer;

        public Server(String port)
        {
            httpListener = new HttpListener();
            serializer = new SerializeJson();
            Url = "http://127.0.0.1:" + port;
            httpListener.Prefixes.Add(Url + "/Ping/");
            httpListener.Prefixes.Add(Url + "/PostInputData/");
            httpListener.Prefixes.Add(Url + "/GetAnswer/");
            httpListener.Prefixes.Add(Url + "/Stop/");
        }

        public void Start()
        {
            Output output = new Output();
            Input input = new Input();

            httpListener.Start();
            Console.WriteLine($"{Url} running");
            while (httpListener.IsListening)
            {
                var context = httpListener.GetContext();
                HttpListenerRequest request = context.Request;

                switch (request.Url.ToString())
                {
                    case "/Ping":
                        Ping(context);
                        break;
                    case "/PostInputData":
                        input = PostInputData(context);
                        break;
                    case "/GetAnswer":
                        output = Actions.GetOutput(input);
                        GetAnswer(context, output);
                        output = new Output();
                        break;
                    case "/Stop":
                        Stop();
                        break;
                    default:
                        break;
                }
            }
        }

        public void Ping(HttpListenerContext context)
        {
            using (StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream))
            {
                streamWriter.Write(HttpStatusCode.OK);
            }
            context.Response.StatusCode = 200;
        }

        public Input PostInputData(HttpListenerContext context)
        {
            using (StreamReader streamReader = new StreamReader(context.Request.InputStream))
            {
                String strReader = streamReader.ReadToEnd();
                Input input = serializer.Deserialize<Input>(strReader);
                return input;
            }
        }

        public void GetAnswer(HttpListenerContext context, Output output)
        {
            string outputDataStr = serializer.Serialize<Output>(output);

            using (StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream))
            {
                streamWriter.Write(outputDataStr);
            }
        }

        public void Stop()
        {
            httpListener.Stop();
        }
    }
}
