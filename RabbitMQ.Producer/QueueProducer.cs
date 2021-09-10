using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    public class QueueProducer
    {

        public static void Publish(IModel channel)
        {
            var arguments = new Dictionary<string, object> { };
            arguments.Add("x-queue-type", "quorum");

            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: arguments);

            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count + {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "", routingKey: "demo-queue", basicProperties: null, body: body);
                count++;
                Thread.Sleep(1000);
                if (count == 20) break;
            }
        }
    }
}
