using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subcriber
{
    class Program
    {
        public static IList<string> allowableCommandLineArgs
           = new[] { "TopicA", "TopicB", "All" };

        static void Main(string[] args)
        {
            //if (args.Length != 1 || !allowableCommandLineArgs.Contains(args[0]))
            //{
            //    Console.WriteLine("Expected one argument, either " +
            //                      "'TopicA', 'TopicB' or 'All'");
            //    Environment.Exit(-1);
            //}

            //string topic = args[0] == "All" ? "" : args[0];
            string topic = "";
            Console.WriteLine("Subscriber started for Topic : {0}", topic);

            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://localhost:12345");
                subSocket.Subscribe(topic);
                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    string messageTopicReceived = subSocket.ReceiveFrameString();
                    string messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine(messageReceived);
                    Console.WriteLine("Task delay");
                    Task.Delay(5000).Wait();
                }
            }
        }
    }
}
