using Microsoft.Extensions.Configuration;

namespace TwoDayDemoBank.Service.Core.Tests.Fixtures
{
    internal interface IConfigurationStrategy
    {
        void OnConfigureAppConfiguration(IConfigurationBuilder configurationBuilder);

        IQueryModelsSeeder CreateSeeder();
    }
}