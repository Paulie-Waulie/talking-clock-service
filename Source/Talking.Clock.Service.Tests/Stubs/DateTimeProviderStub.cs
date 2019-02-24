﻿namespace Talking.Clock.Service.Tests.Stubs
{
    using System;
    using Talking.Clock.Service.DateServices;

    public class DateTimeProviderStub : IDateTimeProvider
    {
        private readonly DateTime now;

        public DateTimeProviderStub(DateTime now)
        {
            this.now = DateTime.SpecifyKind(now, DateTimeKind.Utc);
        }

        public DateTime GetNowInLocalTime()
        {
            return this.now.ToUniversalTime();
        }
    }
}