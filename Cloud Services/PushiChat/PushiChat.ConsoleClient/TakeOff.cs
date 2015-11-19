namespace PushiChat.ConsoleClient
{
    using System;
    using System.Configuration;
    using System.Text;
    using HomeworkHelpers;
    using PubNubMessaging.Core;

    /// <summary>
    /// Implement a very simple chat application based on some message queue service:
    /// Users can send message into a common channel.
    /// Messages are displayed in the format {IP : message_text}.
    /// Implement the application using the PubNub API.
    /// </summary>
    public class TakeOff
    {
        private const ConsoleColor USER_TEXT = ConsoleColor.Blue;
        private const ConsoleColor INFORMATION_TEXT = ConsoleColor.DarkCyan;

        private static bool isWaiting;

        private static readonly StreamHomeworkHelper Helper = new StreamHomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();
            Console.InputEncoding = Encoding.Unicode;

            Helper.ConsoleMio.PrintHeading(" PubNub ChatApp Ip: " + Helper.GetMyIp());

            var pubnub = new Pubnub(
                subscribeKey: ConfigurationManager.AppSettings["subscribeKey"],
                publishKey: ConfigurationManager.AppSettings["publishKey"]);

            Helper.ConsoleMio
                .Write("Press ", INFORMATION_TEXT)
                .Write("Ctrl + X", USER_TEXT)
                .WriteLine(" At any time to quit", INFORMATION_TEXT);

            while (true)
            {
                string message = Helper.ConsoleMio
                    .Write("Enter a new message: ", INFORMATION_TEXT)
                    .ReadInColor(USER_TEXT);

                string ip = Helper.GetMyIp();

                pubnub.Publish<string>(
                    channel: ConfigurationManager.AppSettings["channel"],
                    message: $"{ip} : {message}",
                    userCallback: DisplayReturnMessage,
                    errorCallback: DisplayErrorMessage);

                isWaiting = true;
                while (isWaiting)
                {
                }
            }
        }

        private static void DisplayReturnMessage(string result)
        {
            Helper.ConsoleMio
                .Write("Status: ", INFORMATION_TEXT)
                .WriteLine(result, ConsoleColor.DarkGreen);

            isWaiting = false;
        }

        private static void DisplayErrorMessage(PubnubClientError error)
        {
            Helper.ConsoleMio.WriteLine(error.StatusCode.ToString(), ConsoleColor.Red);
            isWaiting = false;
        }
    }
}
