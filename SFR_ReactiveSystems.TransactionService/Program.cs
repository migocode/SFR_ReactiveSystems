using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SFR_ReactiveSystems.TransactionService;
using SFR_ReactiveSystems.TransactionService.GraphQL;
using System.Reactive.Linq;

Thread.Sleep(2000);

await Host.CreateDefaultBuilder(args)
    .UseSerilog((context, loggerConfig) =>
    {
        loggerConfig.WriteTo.Console();
    })
    .ConfigureServices(services =>
    {
        Uri websocketUri = new Uri("ws://graphql-engine:8080/v1/graphql");

        services.AddStrawberryShakeClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://graphql-engine:8080/v1/graphql"))
            .ConfigureWebSocketClient(client => client.Uri = websocketUri);

        services.AddTransient<GraphQLPaymentClient>();
        services.AddHostedService<Startup>();
    })
    .Build()
    .RunAsync();

await new TaskCompletionSource<object>().Task;
