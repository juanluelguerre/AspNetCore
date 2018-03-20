using System;

namespace ElGuerre.AspNetCore.SampleApi.UnitTests
{
    public interface IServiceProviderAccessor
    {
        IServiceProvider Services { get; }
    }
}
