using ElGuerre.AspNetCore.SampleApi.Services;
using System;
using Xunit;

namespace ElGuerre.AspNetCore.SampleApi.UnitTests
{
    public class ValuesServiceTest : BaseTest
    {
        private readonly IValuesService _valuesServices;

        public ValuesServiceTest(CompositionRootFixture fixture) : base(fixture)
        {
            _valuesServices = (IValuesService)fixture.ServiceProvider.GetService(typeof(IValuesService));           
        }

        [Fact]
        public void GetAllTest()
        {
            var result = _valuesServices.GetAll();

            Assert.NotNull(result);
            //Assert.True(result)
        }
    }
}
