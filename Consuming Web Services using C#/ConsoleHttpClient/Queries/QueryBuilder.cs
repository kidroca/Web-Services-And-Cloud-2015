namespace ConsoleHttpClient.Queries
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;

    public class QueryBuilder
    {
        private static readonly HomeworkHelper Helper = Common.Helper;

        public static Query BuildQueryString()
        {
            string endpoint = GetEndpoint();

            var query = new JusticeQuery(endpoint);

            Helper.ConsoleMio.Write(
                "Do you want to get the content by UUID? Y/N: ", Common.WIZZARD_TEXT);

            if (Console.ReadKey(true).Key != ConsoleKey.Y)
            {
                Helper.ConsoleMio.WriteLine("N", Common.OPTIONS);

                query.PageSize = GetPageSize();
                query.Page = GetPage();
                query.SortByFieldName = GetSorting();
                query.SortOrder = GetOrdering();
            }
            else
            {
                Helper.ConsoleMio.WriteLine("Y", Common.OPTIONS);

                Helper.ConsoleMio.Write("Enter the UUID: ", Common.WIZZARD_TEXT);
                string uuid = Helper.ConsoleMio.ReadInColor(Common.USER_INPUT);
                if (!string.IsNullOrWhiteSpace(uuid) && !uuid.Equals(string.Empty))
                {
                    query.Uuid = uuid;
                }
            }

            query.AddField("title")
                .AddField("url")
                .AddField("uuid");

            Helper.ConsoleMio.Write(
                "Do you want to see the content?(besides the title and url) Y/N: ", Common.WIZZARD_TEXT);
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                Helper.ConsoleMio.WriteLine("Y", Common.OPTIONS);
                query.AddField("body");
            }
            else
            {
                Helper.ConsoleMio.WriteLine("N", Common.OPTIONS);
            }

            Console.WriteLine();
            return query;
        }

        private static SortOrder GetOrdering()
        {
            string message = "Select sorting order: ";
            Helper.ConsoleMio.WriteLine(message, Common.WIZZARD_TEXT);

            var menu = Helper.ConsoleMio.CreateMenu(new List<string>(2))
                .AddItem("Ascending")
                .AddItem("Descending");

            string selected = menu.DisplayMenu(Common.OPTIONS, Common.MENU_HIGHLIGHT);
            PrintChoice(message, selected);

            if (selected.StartsWith("A"))
            {
                return SortOrder.ASC;
            }
            else
            {
                return SortOrder.DESC;
            }
        }

        private static string GetSorting()
        {
            string message = "Sort by: ";
            Helper.ConsoleMio.WriteLine(message, Common.WIZZARD_TEXT);

            var menu = Helper.ConsoleMio.CreateMenu(new List<string>(2))
                .AddItem("Title")
                .AddItem("URL");

            string selected = menu.DisplayMenu(Common.OPTIONS, Common.MENU_HIGHLIGHT);
            PrintChoice(message, selected);

            return selected.ToLower();
        }

        private static int GetPage()
        {
            string message = "Enter page number(skip for default 1): ";
            Helper.ConsoleMio.Write(message, Common.WIZZARD_TEXT);
            int page;
            if (!int.TryParse(Helper.ConsoleMio.ReadInColor(Common.USER_INPUT), out page))
            {
                page = 1;
            }

            PrintChoice(message, page.ToString());

            return page;
        }

        private static int GetPageSize()
        {
            string message = "Enter page size(skip fot default 20): ";
            Helper.ConsoleMio.Write(message, Common.WIZZARD_TEXT);
            int pageSize;
            if (!int.TryParse(Helper.ConsoleMio.ReadInColor(Common.USER_INPUT), out pageSize))
            {
                pageSize = 20;
            }

            PrintChoice(message, pageSize.ToString());

            return pageSize;
        }

        private static string GetEndpoint()
        {
            string message = "What would you like to view: ";
            Helper.ConsoleMio.WriteLine(message, Common.WIZZARD_TEXT);

            var menu = Helper.ConsoleMio.CreateMenu(JusticeQuery.DefaultEndpoints, prefix: "Option");
            string selected = menu.DisplayMenu(Common.OPTIONS, Common.MENU_HIGHLIGHT);

            PrintChoice(message, selected);

            return selected;
        }

        private static void PrintChoice(string message, string choice)
        {
            Console.CursorTop -= 1;
            Console.CursorLeft = message.Length;
            Helper.ConsoleMio.WriteLine(choice, Common.OPTIONS);
        }
    }
}