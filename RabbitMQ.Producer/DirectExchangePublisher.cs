using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            channel.ExchangeDeclare(exchange: "demo-direct-exchange", type: ExchangeType.Direct);
            var count = 0;
            while(true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count + {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "demo-direct-exchange", routingKey: "account.init", basicProperties: null, body: body);
                count++;
                Thread.Sleep(1000);
                if (count == 20) break;
            }
        }
    }
}
