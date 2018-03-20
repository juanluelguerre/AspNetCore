using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace ElGuerre.AspNetCore.SampleApi.IntegrationTests
{
    public class CompositionRootFixture
    {
        private readonly TestServer server;

        public HttpClient Client { get; }

        public CompositionRootFixture()
        {
            this.server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            this.Client = this.server.CreateClient();
        }
    }
}
