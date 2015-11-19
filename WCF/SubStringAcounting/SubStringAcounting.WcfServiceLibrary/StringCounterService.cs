namespace SubStringAcounting.WcfServiceLibrary
{
    using System.Text.RegularExpressions;

    public class StringCounterService : IStringCounterService
    {
        public int CountSubstringOccurence(string subString, string mainString)
        {
            if (string.IsNullOrEmpty(subString) || string.IsNullOrEmpty(mainString))
            {
                return -1;
            }

            Regex pattern = new Regex(subString);
            int count = pattern.Matches(mainString).Count;

            return count;
        }
    }
}
