namespace Messages.RabbitClient
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Text;
    using HomeworkHelpers;
    using RabbitMQ.Client;

    /// <summary>
    /// Implement a very simple chat application based on some message queue service:
    ///     Users can send message into a common channel.
    ///     Messages are displayed in the format {IP : message_text}.
    /// Use a language, cloud and message queue service of your choice (e.g. C# + AppHarbor + IronMQ). Your application 
    /// can be console, GUI or Web-based.
    /// </summary>
    public class TakeOff
    {
        private const ConsoleColor USER_TEXT = ConsoleColor.Blue;
        private const ConsoleColor INFORMATION_TEXT = ConsoleColor.DarkGray;
        private const ConsoleColor SUCCESS = ConsoleColor.DarkGreen;

        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static readonly ConnectionFactory factory = new ConnectionFactory()
        {
            Uri = ConfigurationManager.AppSettings["uri"],
            UserName = ConfigurationManager.AppSettings["username"],
            Password = ConfigurationManager.AppSettings["password"],
        };

        private static string myIpAddress;

        private static void Main()
        {
            Helper.ConsoleMio.Setup();
            Console.InputEncoding = Encoding.Unicode;

            ConsoleKey userchoice;
            do
            {
                Console.Clear();
                Helper.ConsoleMio.PrintHeading("Simple Chat Service -- Chat To Channel");

                string usermessage = Helper.ConsoleMio
                    .Write("Enter your message to the world: ", INFORMATION_TEXT)
                    .ReadInColor(USER_TEXT);

                SendMessage(usermessage);

                Helper.ConsoleMio
                    .Write("Press ", INFORMATION_TEXT)
                    .Write("ESC", USER_TEXT)
                    .WriteLine(" To Exit, or any other key to send a new message", INFORMATION_TEXT);

                userchoice = Console.ReadKey(true).Key;
            }
            while (userchoice != ConsoleKey.Escape);
        }

        private static void SendMessage(string message)
        {
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                        exchange: ConfigurationManager.AppSettings["exchange"],
                        type: ExchangeType.Fanout);

                    string myIp = GetMyIp();
                    var data = Encoding.Unicode.GetBytes($"{myIp} : {message}");

                    IBasicProperties props = channel.CreateBasicProperties();
                    props.Persistent = true;

                    channel.BasicPublish(
                        exchange: ConfigurationManager.AppSettings["exchange"],
                        routingKey: string.Empty,
                        basicProperties: props,
                        body: data);

                    Helper.ConsoleMio.WriteLine("Message Sent.", SUCCESS);
                }
            }
        }

        private static string GetMyIp()
        {
            // кешинг, nice a?
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
    }
}
