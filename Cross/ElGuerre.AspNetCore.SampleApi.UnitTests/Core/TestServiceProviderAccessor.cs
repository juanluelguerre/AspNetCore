using System;
using System.Collections.Generic;
using System.Text;

namespace ElGuerre.AspNetCore.SampleApi.UnitTests
{
    public class TestServiceProviderAccessor : IServiceProviderAccessor
    {
        public TestServiceProviderAccessor(IServiceProvider services)
        {
            this.Services = services;
        }

        public IServiceProvider Services { get; }
    }
}
