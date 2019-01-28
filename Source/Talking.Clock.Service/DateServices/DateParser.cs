namespace Talking.Clock.Service.DateServices
{
    using System;
    using System.Globalization;

    public static class DateParser
    {
        public static bool TryParseFromIso8601(string inputString, out DateTime parsedDateTime)
        {
            var formats = new[] { "yyyy-MM-dd", "yy-MM-dd", "yyyy-MM-ddTHH:mm:ssZ" };
            return DateTime.TryParseExact(inputString, formats, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out parsedDateTime);
        }
    }
}
