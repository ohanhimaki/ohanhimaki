using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ohanhimaki.Web;
using ohanhimaki.Web.Layout;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<MarkdownService.Services.MarkdownService>();
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<PagesService>();

var host = builder.Build();
// Lataa blogipostaukset ennen kuin sovellus alkaa
var pagesService = host.Services.GetRequiredService<PagesService>();
await pagesService.InitializeAsync();

await host.RunAsync();