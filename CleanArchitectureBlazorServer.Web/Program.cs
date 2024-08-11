using CleanArchitectureBlazorServer.Web.Components;
using CleanArchitectureBlazorServer.Infrastructure;
using CleanArchitectureBlazorServer.Application;
using MudBlazor.Services;
using CleanArchitectureBlazorServer.Web.Services;
using CleanArchitectureBlazorServer.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IAccountHoldersService, AccountHoldersService>();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
