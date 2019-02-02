namespace Talking.Clock.Service.Tests.Scenarios
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using Talking.Clock.Service.Tests.Stubs;

    [TestFixture]
    public class TodayTests
    {
        [TestCase("2019-05-20", "Monday")]
        [TestCase("2223-01-01", "Wednesday")]
        public async Task When_Requesting_The_Day_Of_Today_Then_The_Correct_Day_Of_The_Week_Is_Returned(string nowDateString, string expectedDayOfTheWeek)
        {
            var nowDate = DateTime.ParseExact(nowDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dateTimeProvider = new DateTimeProviderStub(nowDate);
            var client = new ClientBuilder().WithDateTimeProvider(dateTimeProvider).Build();
            var result = await client.GetAsync("api/today/day");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsStringAsync()).Should().Be($"Today is {expectedDayOfTheWeek}");
        }

        [TestCase("2019-01-25", "Friday, 25th January 2019")]
        [TestCase("2000-05-20", "Saturday, 20th May 2000")]
        public async Task When_Requesting_Today_Then_The_Date_Is_Returned_In_A_Human_Format(string nowDateString, string expectedDayOfTheWeek)
        {
            var nowDate = DateTime.ParseExact(nowDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dateTimeProvider = new DateTimeProviderStub(nowDate);
            var client = new ClientBuilder().WithDateTimeProvider(dateTimeProvider).Build();
            var result = await client.GetAsync("api/today/date");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsStringAsync()).Should().Be(expectedDayOfTheWeek);
        }
    }
}
