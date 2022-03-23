using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SFR_ReactiveSystems.Frontend.Client;
using SFR_ReactiveSystems.Frontend.Client.GraphQL;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<GraphQLClient>();

await builder.Build().RunAsync();
