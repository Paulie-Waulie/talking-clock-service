namespace Talking.Clock.Service.DateServices
{
    using System;
    using Humanizer;

    public interface IDateService
    {
        string GetDate(DateTime date);

        string GetDateDay(DateTime date);

        string GetTodayDate();

        string GetTodayDay();
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
            return this.GetDate(this.dateTimeProvider.GetUtcNow());
        }

        public string GetTodayDay()
        {
            return this.GetDateDay(this.dateTimeProvider.GetUtcNow());
        }

        public string GetDateDay(DateTime date)
        {
            return date.DayOfWeek.Humanize();
        }
    }
}