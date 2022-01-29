﻿using GeekShopping.MessageBus;
using GeekShopping.OrderAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "172.17.192.1";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            try
            {
                if (ConnectionExists())
                {
                    using var channel = _connection.CreateModel();
                    channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                    byte[] body = GetMessageAsByteArray(message);
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                }
            }
            catch (Exception e)
            {

            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize<PaymentVO>((PaymentVO)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;

        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _userName,
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();

            return _connection != null;
        }

    }
}
