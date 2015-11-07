namespace ConsoleHttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HomeworkHelpers;
    using Queries;

    /// <summary>
    /// Write a console application, which searches for news articles by given a query stringand a 
    /// count of articles to retrieve.
    /// 
    ///     The application should ask the user for input and print the Titles and URLs of the articles.
    /// 
    ///     For news articles search, use the Feedzilla API and use one of WebClient, HttpWebRequest or
    ///     HttpClient.
    /// 
    /// Feedzilla is not responding to any requests so Im using another API
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = Common.Helper;

        private static Requesto requesto = new Requesto(@"http://www.justice.gov/api/v1/");

        private static HashSet<Task<List<Content>>> waitedTasks = new HashSet<Task<List<Content>>>();

        private static void Main()
        {
            do
            {
                Helper.ConsoleMio.Setup();

                Helper.ConsoleMio.PrintHeading("CONSOLE WEB APPLICATION MASTER 5000 ");

                Task<List<Content>> contentTask = GetContentTask();

                if (contentTask != null)
                {
                    if (contentTask.IsCompleted)
                    {
                        DisplayResult(contentTask);
                    }
                    else
                    {
                        waitedTasks.Add(contentTask);
                    }
                }

                HandleOtherCompletedTasks();

                Helper.ConsoleMio.WriteLine("\nContinue? ", Common.OPTIONS);
            }
            while (Helper.ConsoleMio.ReadInColor(Common.USER_INPUT) != "Ne");
        }

        private static void HandleOtherCompletedTasks()
        {
            Task<List<Content>> nextReadyTask;
            while ((nextReadyTask = waitedTasks.FirstOrDefault(t => t.IsCompleted)) != null)
            {
                DisplayResult(nextReadyTask);
                waitedTasks.Remove(nextReadyTask);
            }

            if (waitedTasks.Any())
            {

                Helper.ConsoleMio.WriteLine(
                    "\nTask running in the background\n" +
                    "Submit another request while waiting for the task to complete",
                    Common.WIZZARD_TEXT);
            }
            else
            {
                Helper.ConsoleMio.WriteLine("All tasks are completed!", Common.WIZZARD_TEXT);
            }
        }

        private static Task<List<Content>> GetContentTask()
        {
            string[] options =
            {
                "Option 1: Give me all the things!",
                "Option 2: The other thing.",
                "Option 3: Check the previous thing",
                "Option 0: Exit the thing off"
            };

            string userChoice = GetUserChoice(options);
            Task<List<Content>> contentTask;

            if (userChoice.Equals(options[0]))
            {
                contentTask = GiveAllTheThings();
            }
            else if (userChoice.Equals(options[1]))
            {
                var query = QueryBuilder.BuildQueryString();
                requesto.Query = query;
                contentTask = requesto.GetContent();
            }
            else if (userChoice.Equals(options[2]))
            {
                return null;
            }
            else if (userChoice.Equals(options[3]))
            {
                Helper.ConsoleMio.WriteLine(
                    "Exit? That's out of the question,\n" +
                    "Eat a snickers,\n" +
                    "Now try again... Better?",
                    Common.WIZZARD_TEXT);

                contentTask = GetContentTask();
            }
            else
            {
                throw new NotImplementedException("Nqma Takava Darjava");
            }

            return contentTask;
        }

        private static void DisplayResult(Task<List<Content>> task)
        {
            Console.WriteLine();
            Helper.ConsoleMio.PrintHeading("Response");

            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.InnerExceptions)
                {
                    Helper.ConsoleMio.Write("Error: ", ConsoleColor.Red);
                    Helper.ConsoleMio.WriteLine(exception.Message, ConsoleColor.DarkGray);
                }
            }
            else
            {
                foreach (var content in task.Result)
                {
                    Helper.ConsoleMio.Write("Title: ", ConsoleColor.DarkGreen)
                        .WriteLine(content.Title, ConsoleColor.DarkCyan)
                        .Write("Url: ", ConsoleColor.DarkBlue)
                        .WriteLine(content.Url, ConsoleColor.Blue)
                        .Write("UUID: ", ConsoleColor.DarkGreen)
                        .WriteLine(content.Uuid, ConsoleColor.DarkGray);

                    if (content.Body != null)
                    {
                        Helper.ConsoleMio.WriteLine("Content: ", ConsoleColor.DarkGreen);
                        Helper.ConsoleMio.WriteLine(content.Body, ConsoleColor.DarkGray);
                    }

                    Console.WriteLine();
                }
            }

            Helper.ConsoleMio.PrintHeading(string.Empty);
        }

        private static async Task<List<Content>> GiveAllTheThings()
        {
            var query = new JusticeQuery(JusticeQuery.DefaultEndpoints.First())
                .AddField("title")
                .AddField("url")
                .AddField("uuid");

            requesto.Query = query;
            return await requesto.GetContent();
        }

        private static string GetUserChoice(string[] options)
        {
            Helper.ConsoleMio.WriteLine(
                "Hello I am the query string wizzard!\n" +
                "Let me guide you thorugh the proccess of creating a query string\n",
                Common.WIZZARD_TEXT);

            var menu = Helper.ConsoleMio.CreateMenu(items: options);

            string selected =
                menu.DisplayMenu(foreground: Common.OPTIONS, background: Common.MENU_HIGHLIGHT);

            return selected;
        }
    }
}
