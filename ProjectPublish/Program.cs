using System;
using System.Text;
using RabbitMQ.Client;
using ProjectMessage;

namespace ProjectPublish
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.QueueDeclare(queue: "TestQueue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                for (int i = 0; i < 5; i++) 
                {
                    var message = "Hello" + (i + 1).ToString();
                    var body = Encoding.UTF8.GetBytes(message);
                    
                    channel.BasicPublish(exchange: "",
                                         routingKey: "TestQueue",
                                         basicProperties: properties,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }

            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        //private static string GetMessage(string[] args)
        //{
        //    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        //}
    }
}
