using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

//app.UseMvc();
//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

string text = app.Environment.EnvironmentName;

string test = builder.Configuration.GetValue<string>("MyKey");

app.UseMvcWithDefaultRoute();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapGet("/", async (context) =>
//{
//    throw new Exception(text);
//    await context.Response.WriteAsync(test);
//});

app.Run();