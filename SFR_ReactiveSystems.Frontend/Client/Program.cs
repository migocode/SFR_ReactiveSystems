using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SFR_ReactiveSystems.Frontend.Client;
using SFR_ReactiveSystems.Frontend.Client.GraphQL;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<GraphQLClient>();

Uri baseHttpAddress = new("http://localhost:8080/v1/graphql");
Uri websocketUri = new("ws://localhost:8080/v1/graphql");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
        .AddStrawberryShakeClient()
        .ConfigureHttpClient(client => client.BaseAddress = baseHttpAddress)
        .ConfigureWebSocketClient(client => client.Uri = websocketUri);

await builder.Build().RunAsync();
