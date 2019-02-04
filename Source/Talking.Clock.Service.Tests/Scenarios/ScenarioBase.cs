namespace Talking.Clock.Service.Tests.Scenarios
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Talking.Clock.Service.DateServices;

    public abstract class ScenarioBase
    {
        private HttpResponseMessage responseMessage;
        private string requestUrl;

        protected void WithUrl(string url)
        {
            this.requestUrl = url;
        }

        protected Task MakeTheRequest()
        {
            var client = new ClientBuilder().Build();
            return this.MakeTheRequest(client);
        }

        protected Task MakeTheRequest(IDateTimeProvider dateTimeProvider)
        {
            var client = new ClientBuilder().WithDateTimeProvider(dateTimeProvider).Build();
            return this.MakeTheRequest(client);
        }

        protected async Task MakeTheRequest(HttpClient httpClient)
        {
            this.responseMessage = await httpClient.GetAsync(this.requestUrl);
        }

        protected void TheResponseIsOk()
        {
            this.responseMessage.StatusCode.Should().Be(HttpStatusCode.OK); 
        }

        protected void TheResponseIsNotFound()
        {
            this.responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        protected async Task TheResponseMessageIs(string expectedResponseMessage)
        {
            (await this.responseMessage.Content.ReadAsStringAsync()).Should().Be(expectedResponseMessage);
        }

        protected async Task TheResponseMessageIsEmpty()
        {
            (await this.responseMessage.Content.ReadAsStringAsync()).Should().BeEmpty();
        }
    }
}
