using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random(50);

            using (var pubSocket = new PublisherSocket())
            {
                Console.WriteLine("Publisher socket binding...");
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind("tcp://localhost:12345");

                for (var i = 0; i < 100; i++)
                {
                    var randomizedTopic = rand.NextDouble();
                    if (randomizedTopic > 0.5)
                    {
                        var msg = "TopicA msg-" + i;
                        Console.WriteLine("Sending message : {0}", msg);
                        pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                    }
                    else
                    {
                        var msg = "TopicB msg-" + i;
                        Console.WriteLine("Sending message : {0}", msg);
                        pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                    }

                    Console.WriteLine("ENTER TO SEND MORE MESSAGE");
                    Console.ReadLine();
                }
            }
        }
    }
}
