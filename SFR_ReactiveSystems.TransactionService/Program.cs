using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFR_ReactiveSystems.TransactionService;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Startup>();
    })
    .Build()
    .RunAsync();