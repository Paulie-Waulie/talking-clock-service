namespace Talking.Clock.Service.DateServices
{
    using System;
    using System.Globalization;
    using Humanizer;

    public interface IDateService
    {
        string GetDate(DateTime date);

        string GetDateDay(DateTime date);

        string GetNow();

        string GetDateNow();

        string GetDayNow();


        string GetTimeNow();
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

        public string GetNow()
        {
            return $"{this.GetTimeNow()} on {this.GetDateNow()}";
        }

        public string GetDateNow()
        {
            return this.GetDate(this.dateTimeProvider.GetNowInLocalTime());
        }

        public string GetDayNow()
        {
            return this.GetDateDay(this.dateTimeProvider.GetNowInLocalTime());
        }

        public string GetTimeNow()
        {
            var now = this.dateTimeProvider.GetNowInLocalTime();
            var time = new TwelveHourClockTime(now);

            switch (time.Minute)
            {
                case 0:
                    return $"{time.HourAsWords} O' Clock, {time.Meridiem}";
                case 1:
                    return $"One Minute Past {time.HourAsWords}, {time.Meridiem}";
                case 30:
                    return $"Half Past {time.HourAsWords}, {time.Meridiem}";
                default:
                    return $"{time.MinuteAsWords} Minutes Past {time.HourAsWords}, {time.Meridiem}";
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

            public string HourAsWords => this.Hour.ToWords().Transform(To.TitleCase);

            public int Minute { get; }

            public string MinuteAsWords => this.Minute.ToWords().Transform(To.TitleCase);

            public string Meridiem { get; }
        }
    }
}