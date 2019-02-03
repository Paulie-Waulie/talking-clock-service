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
        public void GetTheTimeNow()
        {
            var now = default(string);
            var expectedDateDescription = default(string);

            this.Given(_ => _.NowIs(now))
                .When(_ => _.AskingForTheDateForToday())
                .Then(_ => _.TheResponseIsOk())
                .And(_ => _.TheResponseMessageIs(expectedDateDescription))
                .WithExamples(new ExampleTable("now", "expectedDateDescription")
                {
                    { "2019-01-25T00:00:00Z", "Twelve O' Clock, AM UTC" },
                    { "2019-01-25T12:00:00Z", "Twelve O' Clock, PM UTC" },
                    { "2019-01-25T01:30:59Z", "Half Past One, AM UTC" },
                    { "2019-01-25T03:01:12Z", "One Minute Past Three, AM UTC" },
                    { "2019-01-25T15:31:00Z", "Thirty-One Minutes Past Three, PM UTC" },
                    { "2019-01-25T23:59:59Z", "Fifty-Nine Minutes Past Eleven, PM UTC" }
                }).BDDfy();
        }

        private void NowIs(string now)
        {
            var nowDate = DateTime.Parse(now);
            this.dateTimeProviderStub = new DateTimeProviderStub(nowDate);
        }


        private async Task AskingForTheDateForToday()
        {
            this.WithUrl("api/now");
            await this.MakeTheRequest(this.dateTimeProviderStub);
        }
    }
}