﻿using Dapper;
using Microsoft.Extensions.DependencyInjection;
using TwoDayDemoBank.Common;

namespace TwoDayDemoBank.Persistence.SQLServer
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSQLServerPersistence(this IServiceCollection services, string connectionString)
        { 
            SqlMapper.AddTypeHandler(new ByteArrayTypeHandler());

            return services.AddSingleton(new SqlConnectionStringProvider(connectionString))
                           .AddSingleton<IAggregateTableCreator, AggregateTableCreator>()
                           .AddSingleton(typeof(IAggregateRepository<,>), typeof(SQLAggregateRepository<,>));
        }
    }
}