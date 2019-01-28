namespace Talking.Clock.Service.Tests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net.Http;
    using Talking.Clock.Service.DateServices;

    internal class ClientBuilder
    {
        private IDateTimeProvider dateTimeProvider;

        internal ClientBuilder WithDateTimeProvider(IDateTimeProvider dateProvider)
        {
            this.dateTimeProvider = dateProvider;
            return this;
        }

        internal HttpClient Build()
        {
            return new TestServer(
                new WebHostBuilder()
                    .UseKestrel()
                    .ConfigureTestServices(collection =>
                    {
                        collection.AddSingleton(this.dateTimeProvider ?? new DateTimeProvider());
                    })
                    .UseStartup<Startup>()).CreateClient();
        }
    }
}
