using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UnitTestingExample
{
    public static class StringUtilities
    {
        public static IEnumerable<string> TopNWords(this string s, int n = 1, char seperator = ' ')
        {
            if (n <= 0)
            {
                throw new ArgumentException($"Parameter must be a positive, non-zero value", nameof(n));
            }

            return s.Split(new[] {seperator}, StringSplitOptions.RemoveEmptyEntries)
                .ToLookup(w => w, StringComparer.CurrentCultureIgnoreCase)
                .Select(w => new {w.Key, Count = w.Count()})
                .OrderByDescending(w => w.Count)
                .ThenBy(w => w.Key)
                .Take(n)
                .Select(w => w.Key);
        }

        public static DateTimeOffset ToDateTimeOffset(this string s)
        {
            return s.ToDateTimeOffset("yyyy-MM-ddTHH:mm:ssK");
        }

        public static DateTimeOffset ToDateTimeOffset(this string s, string datePattern)
        {
            if (datePattern == null)
            {
                throw new ArgumentNullException(nameof(datePattern));
            }

            try
            {
                return DateTimeOffset.ParseExact(s, datePattern, CultureInfo.CurrentUICulture,
                    DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
            }
            catch (FormatException e)
            {
                throw new ArgumentException("The input date or format is invalid", e);
            }
        }
    }
}
