using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace TwoDayDemoBank.Service.Core.Tests.Fixtures
{
    public class WebApiFixture<TStartup> : IDisposable
        where TStartup : class
    {
        private readonly IConfigurationStrategy _configurationStrategy;
        
        public WebApiFixture()
        {
            _configurationStrategy = new OnPremiseConfigurationStrategy();

            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((ctx, configurationBuilder) =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json", false);

                    var aspEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if (!string.IsNullOrWhiteSpace(aspEnv))
                        configurationBuilder.AddJsonFile($"appsettings.{aspEnv}.json", true);

                    configurationBuilder
                        .AddUserSecrets<WebApiFixture<TStartup>>(optional: true)
                        .AddEnvironmentVariables();

                    if (null == _configurationStrategy)
                        throw new Exception("configuration strategy not set");

                    _configurationStrategy.OnConfigureAppConfiguration(configurationBuilder);
                    this.QueryModelsSeeder = _configurationStrategy.CreateSeeder();
                })
                //.UseSerilog()
                .UseStartup<TStartup>();

            var server = new TestServer(builder);
            this.HttpClient = server.CreateClient();
        }

        public void Dispose()
        {
            if (null != this.HttpClient)
            {
                this.HttpClient.Dispose();
                this.HttpClient = null;
            }

            if(_configurationStrategy is IDisposable ds)
                ds.Dispose();
        }

        public HttpClient HttpClient { get; private set; }
        public IQueryModelsSeeder QueryModelsSeeder { get; private set; }
    }
}