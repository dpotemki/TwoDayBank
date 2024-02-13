using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using TwoDayDemoBank.Common;
using TwoDayDemoBank.Domain.DomainEvents;
using TwoDayDemoBank.Domain.Services;
using TwoDayDemoBank.Service.Core.Common;
using TwoDayDemoBank.Service.Core.Common.EventHandlers;
using TwoDayDemoBank.Service.Core.Persistence.Mongo.EventHandlers;
using TwoDayDemoBank.Worker.Core.Registries;

await Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(configurationBuilder =>
    {
        configurationBuilder.AddCommandLine(args);
    })
    .ConfigureAppConfiguration((ctx, builder) =>
    {
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration))
    .ConfigureServices((hostContext, services) =>
    {       
        services.Scan(scan =>
        {
            scan.FromAssembliesOf(typeof(AccountEventsHandler))                
                .RegisterHandlers(typeof(INotificationHandler<>));
        }).Decorate(typeof(INotificationHandler<>), typeof(RetryDecorator<>))
            .AddTransient<ICurrencyConverter, FakeCurrencyConverter>()            
            .AddScoped<IMediator, Mediator>()
            .AddSingleton<IEventSerializer>(new JsonEventSerializer(new[]
            {
                typeof(CustomerEvents.CustomerCreated).Assembly
            }))
            .RegisterInfrastructure(hostContext.Configuration)
            .RegisterWorker();
    })
    .Build()
    .RunAsync();

