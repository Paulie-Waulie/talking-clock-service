namespace Talking.Clock.Service.Tests.Scenarios
{
    using System.Threading.Tasks;
    using NUnit.Framework;
    using TestStack.BDDfy;

    [TestFixture]
    public class DatesTests : ScenarioBase
    {
        private string dateToRequest;

        [Test]
        public void GettingTheDayOfTheWeekForADate()
        {
            var date = default(string);
            var expectedDayOfTheWeek = default(string);

            this.Given(_ => _.TheDateRequestedIs(date))
                .When(_ => _.AskingForTheDayOfTheWeek())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDayOfTheWeek))
                .WithExamples(new ExampleTable("date", "expectedDayOfTheWeek")
                {
                    { "2019-01-25", "Friday" },
                    { "2000-05-20", "Saturday" },
                    { "83-05-20", "Friday" },
                    { "2222-12-31", "Tuesday" },
                    { "2222-12-31T15:00:00Z", "Tuesday" },
                    { "2222-12-31T15%3A00%3A00Z", "Tuesday" }
                }).BDDfy();
        }

        [Test]
        public void GettingDateInReadableFormat()
        {
            var date = default(string);
            var expectedDateDescription = default(string);

            this.Given(_ => _.TheDateRequestedIs(date))
                .When(_ => _.AskingForTheDate())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDateDescription))
                .WithExamples(new ExampleTable("date", "expectedDateDescription")
                {
                    { "2019-01-25", "Friday, 25th January 2019" },
                    { "2000-05-20", "Saturday, 20th May 2000" },
                    { "83-05-20", "Friday, 20th May 1983" },
                    { "1900-02-01", "Thursday, 1st February 1900" },
                    { "1900-02-02", "Friday, 2nd February 1900" },
                    { "1900-02-03", "Saturday, 3rd February 1900" },
                    { "1900-02-04", "Sunday, 4th February 1900" },
                    { "2222-12-31", "Tuesday, 31st December 2222" },
                    { "2222-12-31T15:00:00Z", "Tuesday, 31st December 2222" },
                    { "2222-12-31T15%3A00%3A00Z", "Tuesday, 31st December 2222" }
                }).BDDfy();
        }

        [Test]
        public void InvalidDateFormatReturnsNotFound()
        {
            var date = default(string);

            this.Given(_ => _.TheDateRequestedIs(date))
                .When(_ => _.AskingForTheDate())
                .Then(_ => _.TheResponseIsNotFound())
                .And(_ => _.TheResponseMessageIsEmpty())
                .WithExamples(new ExampleTable("date")
                {
                    { "20-05-2019" },
                    { "20-05-99" },
                    { "1983-35-20" },
                    { "1983-12-32" },
                    { "1983-124-30" },
                    { "1983-12-222" },
                    { "1983-22-01" },
                    { "19830522" },
                    { "1983/05/20" },
                    { "1983.05.20" },
                    { "1983-12-a" },
                    { "aaa" }
                }).BDDfy();
        }

        private void TheDateRequestedIs(string dateString)
        {
            this.dateToRequest = dateString;
        }

        private async Task AskingForTheDayOfTheWeek()
        {
            this.WithUrl($"api/dates/{this.dateToRequest}/day");
            await this.MakeTheRequest();
        }

        private async Task AskingForTheDate()
        {
            this.WithUrl($"api/dates/{this.dateToRequest}/date");
            await this.MakeTheRequest();
        }
    }
}