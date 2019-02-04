namespace Talking.Clock.Service.DateServices
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime GetNowInLocalTime();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNowInLocalTime()
        {
            return DateTime.Now.ToLocalTime();
        }
    }
}
