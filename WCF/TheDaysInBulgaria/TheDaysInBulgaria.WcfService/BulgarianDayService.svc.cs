namespace TheDaysInBulgaria.WcfService
{
    using System;
    using System.Globalization;

    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BulgarianDayService : IBulgarianDayService
    {
        private static readonly CultureInfo Culture = new CultureInfo("bg-BG");

        public string GetDayName(DateTime date)
        {
            string day = Culture.DateTimeFormat.GetDayName(date.DayOfWeek);

            return day;
        }
    }
}
