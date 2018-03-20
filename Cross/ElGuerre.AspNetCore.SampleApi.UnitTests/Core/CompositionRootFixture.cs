using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElGuerre.AspNetCore.SampleApi.UnitTests
{
    public class CompositionRootFixture
    {
        protected readonly IServiceCollection Services;
        public IServiceProvider ServiceProvider { get; }
        public IConfigurationRoot Configuration { get; }

        // TODO:
        // public DataContext Context { get; }

        public CompositionRootFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            this.Configuration = builder.Build();
            this.Services = new ServiceCollection();
            this.ConfigureServices();
            this.ServiceProvider = this.Services.BuildServiceProvider();

            // TODO:
            //Context = this.ServiceProvider.GetService<DataContext>();
            //Context.Seed();
        }

        private void ConfigureServices()
        {
            // TODO: Add services as needed

            this.Services.Add(new ServiceDescriptor(typeof(IServiceProviderAccessor), typeof(TestServiceProviderAccessor), ServiceLifetime.Singleton));
            this.Services.AddTransient(x => this.ServiceProvider);
        }
    }
}
