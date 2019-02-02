namespace Talking.Clock.Service.Tests.Scenarios
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Talking.Clock.Service.Tests.Stubs;
    using TestStack.BDDfy;

    [TestFixture]
    public class TodayTests : ScenarioBase
    {
        private DateTimeProviderStub dateTimeProviderStub;

        [Test]
        public void GettingTodayDayOfTheWeek()
        {
            var dateToday = default(string);
            var expectedDayOfTheWeek = default(string);

            this.Given(_ => _.TodayIs(dateToday))
                .When(_ => _.AskingForTheDayOfTheWeekForToday())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDayOfTheWeek))
                .WithExamples(new ExampleTable("dateToday", "expectedDayOfTheWeek")
                {
                    { "2019-05-20", "Today is Monday" },
                    { "2223-01-01", "Today is Wednesday" }
                }).BDDfy();
        }

        [Test]
        public void GettingTodayDate()
        {
            var dateToday = default(string);
            var expectedDateDescription = default(string);

            this.Given(_ => _.TodayIs(dateToday))
                .When(_ => _.AskingForTheDateForToday())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDateDescription))
                .WithExamples(new ExampleTable("dateToday", "expectedDateDescription")
                {
                    { "2019-01-25", "Friday, 25th January 2019" },
                    { "2000-05-20", "Saturday, 20th May 2000" }
                }).BDDfy();
        }

        private void TodayIs(string dateToday)
        {
            var nowDate = DateTime.ParseExact(dateToday, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            this.dateTimeProviderStub = new DateTimeProviderStub(nowDate);
        }

        private async Task AskingForTheDayOfTheWeekForToday()
        {
            this.WithUrl("api/today/day");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }

        private async Task AskingForTheDateForToday()
        {
            this.WithUrl("api/today/date");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }
    }
}