using System;
using System.Text.RegularExpressions;

namespace ManagerHelper.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Use this to parse text that contains a 4 digit year and a quarter in the form of Q# or q#
        /// </summary>
        /// <param name="quarterText"></param>
        /// <param name="quarter"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool TryParseQuarterString(this string quarterText, out int quarter, out int year)
        {
            var quarterRegex = "q|Q[0-9]";
            var yearRegex = "[0-9][0-9][0-9][0-9]";

            quarter = -1;
            year = -1;

            try
            {
                if (!Regex.IsMatch(quarterText, quarterRegex) || !Regex.IsMatch(quarterText, yearRegex))
                    return false;

                var quarterPart = Regex.Match(quarterText, quarterRegex).Value;

                var qParseResult = int.TryParse(Regex.Match(quarterPart, @"\d+").Value, out int quarterNumber);

                if (!qParseResult) return false;

                var yParseResult = int.TryParse(Regex.Match(quarterText, yearRegex).Value, out int yearNumber);

                if (!yParseResult) return false;

                quarter = quarterNumber;
                year = yearNumber;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
