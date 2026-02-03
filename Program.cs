using SuperBowlSquares.Components;
using SuperBowlSquares.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure port for Railway (and other hosts)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<GameStateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // HTTPS redirection disabled for Railway - they handle SSL
    // app.UseHsts();
}

// HTTPS redirection disabled for Railway - they handle SSL at the edge
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// SignalR removed - real-time updates now use the singleton GameStateService

app.Run();