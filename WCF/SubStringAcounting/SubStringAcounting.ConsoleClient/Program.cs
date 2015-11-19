namespace SubStringAcounting.ConsoleClient
{
    using System;
    using HomeworkHelpers;

    /// <summary>
    /// Create a console client for the WCF service above.
    ///     -- Use the svcutil.exe tool to generate the proxy classes.
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Substring Counting Service, You've come to the right place");

            var service = new StringCounterServiceClient();

            do
            {
                string pattern = Helper.ConsoleMio.Write("Enter the first string: ", ConsoleColor.DarkCyan)
                .ReadInColor(ConsoleColor.Blue);

                string mainString = Helper.ConsoleMio.Write("Enter the second string: ", ConsoleColor.DarkCyan)
                    .ReadInColor(ConsoleColor.Blue);

                int result = service.CountSubstringOccurence(pattern, mainString);

                Helper.ConsoleMio
                    .Write("'{0}'", ConsoleColor.DarkBlue, mainString)
                    .Write(" contains ", ConsoleColor.DarkCyan)
                    .Write("'{0}'", ConsoleColor.DarkRed, pattern)
                    .WriteLine(" {0} time{1}", ConsoleColor.Blue, result, result == 1 ? string.Empty : "s")
                    .WriteLine("\nPress 'Esc' to exit or other key to repeat...\n", ConsoleColor.DarkGray);
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            service.Close();

            Environment.Exit(0);
        }
    }
}
