namespace Talking.Clock.Service.Tests.Scenarios
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class DatesTests
    {
        [TestCase("25-01-2019", "Friday")]
        [TestCase("20-05-2000", "Saturday")]
        [TestCase("20-05-83", "Friday")]
        [TestCase("31-12-2222", "Tuesday")]
        public async Task When_Requesting_The_Day_Of_A_Date_Then_The_Correct_Day_Of_The_Week_Is_Returned(string dateString, string expectedDayOfTheWeek)
        {
            var client = ClientBuilder.Build();
            var result = await client.GetAsync($"api/dates/{dateString}/day");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsStringAsync()).Should().Be(expectedDayOfTheWeek);
        }
    }
}