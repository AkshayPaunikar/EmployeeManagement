using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireNonAlphanumeric=false;
//    options.Password.RequiredLength = 0;
//    options.Password.RequireDigit=false;
//    options.Password.RequireLowercase=false;
//    options.Password.RequireUppercase=false;
//});

builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection")));

//builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
builder.Services.AddMvc(config =>
{
    config.EnableEndpointRouting = false;
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
//builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

//app.UseMvc();
//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

string text = app.Environment.EnvironmentName;

string test = builder.Configuration.GetValue<string>("MyKey");

app.UseAuthentication();

app.UseMvcWithDefaultRoute();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapGet("/environment", async (context) =>
{
    //throw new Exception(text);
    await context.Response.WriteAsync(text);
});

app.Run();