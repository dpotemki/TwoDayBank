using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TwoDayDemoBank.Service.Core.Tests.Fixtures
{
    public class OnPremiseConfigurationStrategy : IConfigurationStrategy, IDisposable
    {
        private string _queryDbName;
        private string _queryDbConnectionString;
        
        public void OnConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("queryDbName", $"bankAccounts_queries_{Guid.NewGuid()}"),                
                new KeyValuePair<string, string>("eventsTopicName", $"events_{Guid.NewGuid()}")
            });

            var cfg = configurationBuilder.Build();
            _queryDbName = cfg["queryDbName"];
            _queryDbConnectionString = cfg.GetConnectionString("mongo");            
        }

        public void Dispose()
        {
            if (!string.IsNullOrWhiteSpace(_queryDbConnectionString))
            {
                var client = new MongoClient(_queryDbConnectionString);
                client.DropDatabase(_queryDbName);          
            }
        }

        public IQueryModelsSeeder CreateSeeder() => new OnPremiseQueryModelsSeeder(_queryDbConnectionString, _queryDbName);
    }
}
