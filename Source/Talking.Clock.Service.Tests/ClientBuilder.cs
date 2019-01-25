namespace Talking.Clock.Service.Tests
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    internal static class ClientBuilder
    {
        internal static HttpClient Build()
        {
            return new TestServer(new WebHostBuilder().UseKestrel().UseStartup<Startup>()).CreateClient();
        }
    }
}
