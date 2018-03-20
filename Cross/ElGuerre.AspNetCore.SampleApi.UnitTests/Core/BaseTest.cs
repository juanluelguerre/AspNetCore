using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ElGuerre.AspNetCore.SampleApi.UnitTests
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
