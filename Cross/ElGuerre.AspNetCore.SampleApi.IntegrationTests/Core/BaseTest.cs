using Xunit;

namespace ElGuerre.AspNetCore.SampleApi.IntegrationTests
{
    public class BaseTest : IClassFixture<CompositionRootFixture>
    {
        protected readonly CompositionRootFixture Fixture;

        public BaseTest(CompositionRootFixture fixture)
        {
            this.Fixture = fixture;
        }
    }
}
