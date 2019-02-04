namespace Talking.Clock.Service.DateServices
{
    using System;
    using System.Globalization;
    using Humanizer;

    public interface IDateService
    {
        string GetDate(DateTime date);

        string GetDateDay(DateTime date);

        string GetTodayDate();

        string GetTodayDay();

        string GetNow();
    }

    internal class DateService : IDateService
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public DateService(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public string GetDate(DateTime date)
        {
            return $"{date.DayOfWeek.Humanize()}, {date.ToOrdinalWords()}";
        }

        public string GetTodayDate()
        {
            return this.GetDate(this.dateTimeProvider.GetNowInLocalTime());
        }

        public string GetTodayDay()
        {
            return this.GetDateDay(this.dateTimeProvider.GetNowInLocalTime());
        }

        public string GetNow()
        {
            var now = this.dateTimeProvider.GetNowInLocalTime();
            var time = new TwelveHourClockTime(now);

            switch (time.Minute)
            {
                case 0:
                    return $"{time.Hour.ToWords().Transform(To.TitleCase)} O' Clock, {time.Meridiem}";
                case 1:
                    return $"One Minute Past {time.Hour.ToWords().Transform(To.TitleCase)}, {time.Meridiem}";
                case 30:
                    return $"Half Past {time.Hour.ToWords().Transform(To.TitleCase)}, {time.Meridiem}";
                default:
                    return $"{time.Minute.ToWords().Transform(To.TitleCase)} Minutes Past {time.Hour.ToWords().Transform(To.TitleCase)}, {time.Meridiem}";
            }
        }

        public string GetDateDay(DateTime date)
        {
            return date.DayOfWeek.Humanize();
        }

        private class TwelveHourClockTime
        {
            public TwelveHourClockTime(DateTime time)
            {
                this.Hour = time.Hour;
                this.Minute = time.Minute;
                if (this.Hour > 11)
                {
                    this.Hour -= 12;
                }

                if (this.Hour.Equals(0))
                {
                    this.Hour = 12;
                }

                this.Meridiem = time.ToString("tt", CultureInfo.InvariantCulture);
            }

            public int Hour { get; }

            public int Minute { get; }

            public string Meridiem { get; }
        }
    }
}