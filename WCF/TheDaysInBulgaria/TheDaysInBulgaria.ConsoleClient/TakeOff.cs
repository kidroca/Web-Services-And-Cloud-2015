namespace TheDaysInBulgaria.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;
    using ServiceReference;

    /// <summary>
    /// Create a console-based client for the WCF service above. Use the "Add Service Reference" in Visual Studio.
    /// </summary>
    public class TakeOff
    {
        private const ConsoleColor WizzardText = ConsoleColor.DarkMagenta;
        private const ConsoleColor UserInput = ConsoleColor.Blue;
        private const ConsoleColor MenuForeground = ConsoleColor.DarkCyan;
        private const ConsoleColor MenuBackground = ConsoleColor.Gray;
        private const ConsoleColor Result = ConsoleColor.DarkGreen;
        private const string ResultTemplate = "Денят е: {0}";
        private const string ExitMessage = "\n Натисни 'Esc' за да излезеш, или друг клавиш за да опиташ отново.";

        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static readonly string[] Months =
        {
            "Януар",
            "Февруарито",
            "Мартуй",
            "Априлец",
            "Майчи",
            "Юний",
            "Юуйлеле",
            "АГУ",
            "Себтрембр",
            "Окотомври",
            "Нормандий",
            "Деметри"
        };

        private static void Main()
        {
            // Besides everithing else Console is set to Unicode to be able to display proper cyrilic characters
            // Console.OutputEncoding = Encoding.Unicode;
            Helper.ConsoleMio.Setup();
            Helper.ConsoleMio.PrintHeading("The Days Of Bulgaria Console Client ");

            var bgService = new BulgarianDayServiceClient();

            do
            {
                DateTime selected = SelectDate();
                string dayName = bgService.GetDayName(selected);

                Helper.ConsoleMio
                    .WriteLine(ResultTemplate, Result, dayName)
                    .WriteLine(ExitMessage, WizzardText)
                    .PrintHeading("");
            }
            while (Console.ReadKey(true).Key != ConsoleKey.X);

            bgService.Close();
            Environment.Exit(0);
        }

        private static DateTime SelectDate()
        {
            int currsorTop = Console.CursorTop;
            string monthMessage = "Изберете месец: ";

            Helper.ConsoleMio.WriteLine(monthMessage, WizzardText);

            string selectedMonth = Helper.ConsoleMio.CreateMenu(Months)
                .DisplayMenu(MenuForeground, MenuBackground);

            Console.SetCursorPosition(left: monthMessage.Length, top: currsorTop);
            Helper.ConsoleMio.WriteLine(selectedMonth, UserInput);

            int month = Array.IndexOf(Months, selectedMonth) + 1;

            var daysMenu = Helper.ConsoleMio.CreateMenu(new List<int>());
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, month); i++)
            {
                daysMenu.AddItem(i);
            }

            currsorTop = Console.CursorTop;
            string dayMessage = "Изберете ден: ";

            Helper.ConsoleMio.WriteLine(dayMessage, WizzardText);

            int day = daysMenu.DisplayMenu(MenuForeground, MenuBackground);

            Console.SetCursorPosition(left: dayMessage.Length, top: currsorTop);
            Helper.ConsoleMio.WriteLine(day.ToString(), UserInput);

            var date = new DateTime(DateTime.Now.Year, month, day);

            return date;
        }
    }
}
