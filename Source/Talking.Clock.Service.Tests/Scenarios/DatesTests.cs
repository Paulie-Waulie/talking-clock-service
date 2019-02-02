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

        [TestCase("2019-01-25", "Friday, 25th January 2019")]
        [TestCase("2000-05-20", "Saturday, 20th May 2000")]
        [TestCase("83-05-20", "Friday, 20th May 1983")]
        [TestCase("1900-02-01", "Thursday, 1st February 1900")]
        [TestCase("1900-02-02", "Friday, 2nd February 1900")]
        [TestCase("1900-02-03", "Saturday, 3rd February 1900")]
        [TestCase("1900-02-04", "Sunday, 4th February 1900")]
        [TestCase("2222-12-31", "Tuesday, 31st December 2222")]
        [TestCase("2222-12-31T15:00:00Z", "Tuesday, 31st December 2222")]
        [TestCase("2222-12-31T15%3A00%3A00Z", "Tuesday, 31st December 2222")]
        public async Task When_Requesting_A_Date_Then_The_Date_Is_Returned_In_A_Human_Format(string dateString, string expectedDateDescription)
        {
            var client = new ClientBuilder().Build();
            var result = await client.GetAsync($"api/dates/{dateString}/date");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsStringAsync()).Should().Be(expectedDateDescription);
        }
    }
}