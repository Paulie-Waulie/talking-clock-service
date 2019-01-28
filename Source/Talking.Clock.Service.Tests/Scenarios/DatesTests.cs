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
    public class DatesTests
    {
        [TestCase("2019-01-25", "Friday")]
        [TestCase("2000-05-20", "Saturday")]
        [TestCase("83-05-20", "Friday")]
        [TestCase("2222-12-31", "Tuesday")]
        [TestCase("2222-12-31T15:00:00Z", "Tuesday")]
        [TestCase("2222-12-31T15%3A00%3A00Z", "Tuesday")]
        public async Task When_Requesting_The_Day_Of_A_Date_Then_The_Correct_Day_Of_The_Week_Is_Returned(string dateString, string expectedDayOfTheWeek)
        {
            var client = new ClientBuilder().Build();
            var result = await client.GetAsync($"api/dates/{dateString}/day");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsStringAsync()).Should().Be(expectedDayOfTheWeek);
        }

        [TestCase("2019-05-20", "Monday")]
        [TestCase("2223-01-01", "Wednesday")]
        public async Task When_Requesting_The_Day_Of_Today_Then_The_Correct_Day_Of_The_Week_Is_Returned(string nowDateString, string expectedDayOfTheWeek)
        {
            var nowDate = DateTime.ParseExact(nowDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dateTimeProvider = new DateTimeProviderStub(nowDate);
            var client = new ClientBuilder().WithDateTimeProvider(dateTimeProvider).Build();
            var result = await client.GetAsync($"api/dates/today/day");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsStringAsync()).Should().Be(expectedDayOfTheWeek);
        }
    }
}