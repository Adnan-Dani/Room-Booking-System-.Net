using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RMS.Controllers;
using RMS.DataAccess;
using RMS.DataAccess.Repository;
using RMS.DataAccess.Repository.IRepository;
using RMS.Middleware;
using RMS.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


// Global Exception
builder.Services.AddTransient<GlobalExceptionMiddleware>();

// Logs Configuration
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
//var _logger = new LoggerConfiguration().WriteTo.File("C:\\Users\\Adnan\\Desktop\\CodeHubSys\\Room-Booking-System-.Net\\logs-.log", rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Logging.AddSerilog(_logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//using GlobalException middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
