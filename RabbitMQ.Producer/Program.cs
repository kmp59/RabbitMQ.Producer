using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                //URI Formula: amqp://{USERNAME}:{PASSWORD}@{HOSTNAME}:{PORTNUMBER}
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using var conn = factory.CreateConnection();

            using (var channel = conn.CreateModel())
            {
                var arguments = new Dictionary<string, object> { };
                arguments.Add("x-queue-type", "quorum");

                channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: arguments);
                for(int i = 0; i < 10; i++)
                {
                    var message = new { Name = "Producer", Message = $"Hello+{i}" };
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(exchange: "", routingKey: "demo-queue", basicProperties: null, body: body);
                }
            }
        }
    }
}
