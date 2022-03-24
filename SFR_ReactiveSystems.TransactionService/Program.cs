using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SFR_ReactiveSystems.TransactionService;
using System.Reactive.Linq;

Thread.Sleep(2000);

await Host.CreateDefaultBuilder(args)
    .UseSerilog((context, loggerConfig) =>
    {
        loggerConfig.WriteTo.Console();
    })
    .ConfigureServices(services =>
    {
        Uri websocketUri;
        Uri baseHttpAddress;
        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        {
            websocketUri = new Uri("ws://graphql-engine:8080/v1/graphql");
            baseHttpAddress = new Uri("http://graphql-engine:8080/v1/graphql");
        }
        else
        {
            websocketUri = new Uri("ws://localhost:8080/v1/graphql");
            baseHttpAddress = new Uri("http://localhost:8080/v1/graphql");
        }

        services.AddStrawberryShakeClient()
            .ConfigureHttpClient(client => client.BaseAddress = baseHttpAddress)
            .ConfigureWebSocketClient(client => client.Uri = websocketUri);

        services.AddHostedService<Startup>();
    })
    .Build()
    .RunAsync();
