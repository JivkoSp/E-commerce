using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksPlace.Data.RabbitConnection
{
    public interface IBooksPlaceConnectionFactory
    {
        IConnection GetConnection();
    }

    public class BooksPlaceConnectionFactory : IBooksPlaceConnectionFactory
    {
        private ConnectionFactory factory;

        public BooksPlaceConnectionFactory()
        {
            factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        public IConnection GetConnection()
        {
            return factory.CreateConnection();
        }
    }

}
