using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ElGuerre.AspNetCore.SampleApi.IntegrationTests
{
    public class ValuesControllerTest : BaseTest
    {
        private string urlApi = "/api/values/";

        public ValuesControllerTest(CompositionRootFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetAllTest()
        {
            var response = await this.Fixture.Client.GetAsync($"{urlApi}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
