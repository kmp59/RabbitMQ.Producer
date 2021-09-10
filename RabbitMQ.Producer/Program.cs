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
                DirectExchangePublisher.Publish(channel);
            }
        }
    }
}
