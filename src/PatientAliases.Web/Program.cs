using PatientAliases.Web.Components;
using PatientAliases.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server with interactive server components.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Application services (swap the in-memory impl for a real repo in production).
builder.Services.AddSingleton<IPatientAliasesService, InMemoryPatientAliasesService>();

builder.Services.AddLogging();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
