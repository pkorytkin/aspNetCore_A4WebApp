using A4WebApp.Middleware;
using A4WebApp.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Logging.AddConsole();
builder.Services.AddControllers();
builder.Services.AddDatabaseConnection();
var app = builder.Build();
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("/");
    }
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware(typeof(RedirectToIndex));
app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();



app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Books}/{action=Index}");
});
app.Run();
