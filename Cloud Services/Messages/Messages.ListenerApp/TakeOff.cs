namespace Messages.ListenerApp
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;
    using System.Text;
    using HomeworkHelpers;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class TakeOff
    {
        private const ConsoleColor USER_IP_COLOR = ConsoleColor.DarkBlue;
        private const ConsoleColor USER_TEXT = ConsoleColor.Blue;

        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static readonly Queue<ConsoleColor> AvailableColors = new Queue<ConsoleColor>();

        static readonly ConnectionFactory connFactory = new ConnectionFactory
        {
            Uri = ConfigurationManager.AppSettings["uri"]
        };

        private static string myIpAddress;

        private static void Main()
        {
            Initialize();

            using (var connection = connFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare
                        (exchange: ConfigurationManager.AppSettings["exchange"],
                        type: ExchangeType.Fanout);

                    string queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(
                        queue: queueName,
                        exchange: ConfigurationManager.AppSettings["exchange"],
                        routingKey: String.Empty);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        string message = Encoding.Unicode.GetString(ea.Body);
                        ProccessMessage(message);
                    };

                    channel.BasicConsume(
                        queue: queueName,
                        noAck: true,
                        consumer: consumer);

                    Helper.ConsoleMio.WriteLine("Press 'Esc' at any time to exit...", ConsoleColor.DarkRed);

                    while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                    {
                    }
                }
            }
        }

        private static void Initialize()
        {
            Helper.ConsoleMio.Setup();
            Helper.ConsoleMio.PrintHeading("Chat Channel");

            AvailableColors.Enqueue(ConsoleColor.Cyan);
            AvailableColors.Enqueue(ConsoleColor.DarkCyan);
            AvailableColors.Enqueue(ConsoleColor.Green);
            AvailableColors.Enqueue(ConsoleColor.DarkGreen);
        }

        private static string GetMyIp()
        {
            if (myIpAddress == null)
            {
                using (var webClient = new WebClient())
                {
                    string ip = webClient.DownloadString("http://icanhazip.com/").Trim();
                    if (Char.IsDigit(ip[0]))
                    {
                        myIpAddress = ip;
                    }
                    else
                    {
                        myIpAddress = "private";
                    }
                }
            }

            return myIpAddress;
        }

        private static void ProccessMessage(string message)
        {
            int indexSemicolon = message.IndexOf(':');

            string ip = message.Substring(0, indexSemicolon).Trim();
            string content = message.Substring(indexSemicolon + 1).Trim();

            ConsoleColor ipColor;
            ConsoleColor contentColor;
            if (ip.Equals(GetMyIp()))
            {
                ip = "me";
                ipColor = USER_IP_COLOR;
                contentColor = USER_TEXT;
            }
            else
            {
                ipColor = AvailableColors.Dequeue();
                contentColor = AvailableColors.Dequeue();

                AvailableColors.Enqueue(ipColor);
                AvailableColors.Equals(contentColor);
            }

            Helper.ConsoleMio.Write(ip, ipColor)
                .Write(" : ", ConsoleColor.DarkGray)
                .WriteLine(content, contentColor);
        }
    }
}
