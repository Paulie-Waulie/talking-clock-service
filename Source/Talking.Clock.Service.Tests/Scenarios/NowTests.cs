namespace Talking.Clock.Service.Tests.Scenarios
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Talking.Clock.Service.Tests.Stubs;
    using TestStack.BDDfy;

    [TestFixture]
    public class NowTests : ScenarioBase
    {
        private DateTimeProviderStub dateTimeProviderStub;

        [Test]
        public void GetNow()
        {
            var now = default(string);
            var expectedDateDescription = default(string);

            this.Given(_ => _.NowIs(now))
                .When(_ => _.AskingForNow())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDateDescription))
                .WithExamples(new ExampleTable("now", "expectedDateDescription")
                {
                    { "2019-01-25T00:00:00", "Twelve O' Clock, AM on Friday, 25th January 2019" },
                    { "2019-01-25T12:00:00", "Twelve O' Clock, PM on Friday, 25th January 2019" },
                    { "2000-05-20T01:30:59", "Half Past One, AM on Saturday, 20th May 2000" },
                    { "2223-01-01T03:01:12", "One Minute Past Three, AM on Wednesday, 1st January 2223" },
                    { "2019-01-25T15:31:00", "Thirty-One Minutes Past Three, PM on Friday, 25th January 2019" },
                    { "2019-01-25T23:59:59", "Fifty-Nine Minutes Past Eleven, PM on Friday, 25th January 2019" }
                }).BDDfy();
        }

        [Test]
        public void GetTheTimeNow()
        {
            var now = default(string);
            var expectedDateDescription = default(string);

            this.Given(_ => _.NowIs(now))
                .When(_ => _.AskingForTheTimeNow())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDateDescription))
                .WithExamples(new ExampleTable("now", "expectedDateDescription")
                {
                    { "2019-01-25T00:00:00", "Twelve O' Clock, AM" },
                    { "2019-01-25T12:00:00", "Twelve O' Clock, PM" },
                    { "2019-01-25T01:30:59", "Half Past One, AM" },
                    { "2019-01-25T03:01:12", "One Minute Past Three, AM" },
                    { "2019-01-25T15:31:00", "Thirty-One Minutes Past Three, PM" },
                    { "2019-01-25T23:59:59", "Fifty-Nine Minutes Past Eleven, PM" }
                }).BDDfy();
        }

        [Test]
        public void GettingTodayDayOfTheWeek()
        {
            var dateToday = default(string);
            var expectedDayOfTheWeek = default(string);

            this.Given(_ => _.NowIs(dateToday))
                .When(_ => _.AskingForTheDayOfTheWeekNow())
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

            this.Given(_ => _.NowIs(dateToday))
                .When(_ => _.AskingForTheDateNow())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDateDescription))
                .WithExamples(new ExampleTable("dateToday", "expectedDateDescription")
                {
                    { "2019-01-25", "Friday, 25th January 2019" },
                    { "2000-05-20", "Saturday, 20th May 2000" }
                }).BDDfy();
        }

        private void NowIs(string now)
        {
            var nowDate = DateTime.Parse(now);
            this.dateTimeProviderStub = new DateTimeProviderStub(nowDate);
        }

        private async Task AskingForNow()
        {
            this.WithUrl("api/now");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }


        private async Task AskingForTheDateNow()
        {
            this.WithUrl("api/now/date");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }

        private async Task AskingForTheTimeNow()
        {
            this.WithUrl("api/now/time");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }

        private async Task AskingForTheDayOfTheWeekNow()
        {
            this.WithUrl("api/now/day");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }
    }
}