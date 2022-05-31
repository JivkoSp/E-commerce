using BooksPlace.Data.RabbitConnection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using System.Text.Json.Serialization;
using BooksPlace.Models;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace BooksPlace.MessageBroker
{
    public class RabbitMqHub
    {
        private IBooksPlaceConnectionFactory factory;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();

        public RabbitMqHub(IBooksPlaceConnectionFactory factory)
        {
            this.factory = factory;
        }

        public void Publish<T>(T Message)
        {
            try
            {
                var connection = factory.GetConnection();

                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "BooksPlaceExchange", type: "direct", durable: false, autoDelete: false, arguments: null);

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    };

                    channel.BasicPublish(exchange: "BooksPlaceExchange", routingKey: "key1", 
                        basicProperties: null, body: JsonSerializer.SerializeToUtf8Bytes(Message, options));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ReviewComment Pull(string userName)
        {
            ReviewComment message = null;
            try
            {
                var connection = factory.GetConnection();

                using (var channel = connection.CreateModel())
                {
                    BasicGetResult result = channel.BasicGet(queue: userName, autoAck: true);

                    if (result != null)
                    {
                        string jsonObject = Encoding.UTF8.GetString(result.Body.ToArray());

                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve
                        };

                        message = JsonSerializer.Deserialize<ReviewComment>(jsonObject, options);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return message;
        }
    }
}
