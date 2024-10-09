//using BlazorAppLinkShort.Client.Pages;
using Domain.Handlers;
using Domain.Infra.Repositories;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using TestUpload.Domain.Infra.Data;
using BlazorAppLinkShort;
using BlazorAppLinkShort.Components;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<UploadFileHandler>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
//});


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7095/")
});

#region DbContext
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 4, 0))));
#endregion 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//ATTENTION: support anti counterfeiting. find out more: https://learn.microsoft.com/en-us/aspnet/core/blazor/security/?view=aspnetcore-8.0
app.UseAntiforgery();

app.MapControllers();
app.UseStaticFiles();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppLinkShort.Client._Imports).Assembly);

app.Run();
