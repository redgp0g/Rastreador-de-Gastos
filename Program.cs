using Microsoft.EntityFrameworkCore;
using Rastreador_de_Gastos.Models;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQ2OTk5MkAzMjMxMmUzMTJlMzMzNU01NFY1WEN1SEZ0eUhIOFBWQmVqM3B5TlF0SFI5MzhmZ01oaExHWURrZ0E9;Mgo DSMBaFt QHFqVkNrWE5FaV1CX2BZd1lzQmldek4QCV5EYF5SRHVdR19iTHlXc0FmWHc=;Mgo DSMBMAY9C3t2VFhhQlJBfVtdXHxLflF1VWpTell6cFBWACFaRnZdQV1gS3tRd0BkXXpadnxd;Mgo DSMBPh8sVXJ1S0d X1RPckBDQmFJfFBmTGlceFR0cUUmHVdTRHRcQl5hTHxSc0JnXHtWcnw=;MTQ2OTk5NkAzMjMxMmUzMTJlMzMzNVpyWkNDMTRXU3BQdURvQ0ZPOW43YXVFcGo4U1ZCVldGV3FzbjBZSnNMNkU9;NRAiBiAaIQQuGjN/V0d XU9Hc1RGQmJWfFN0RnNQdVt3flFDcDwsT3RfQF5jSnxWdERmXntbdXZUTg==;ORg4AjUWIQA/Gnt2VFhhQlJBfVtdXHxLflF1VWpTell6cFBWACFaRnZdQV1gS3tRd0BkXXpbc3Fd;MTQ2OTk5OUAzMjMxMmUzMTJlMzMzNUQ3elAyYzlsUG9ENWpxTXJtcUtoUENYWElLNHg1ZDRuaUlUdEVrYVkrZTA9;MTQ3MDAwMEAzMjMxMmUzMTJlMzMzNUdjaVllSDR5QzVsOHFLeTY2VWMvS0N6Q3dRZFM4VnVKVjNaNDVCODFPaGc9;MTQ3MDAwMUAzMjMxMmUzMTJlMzMzNUgzTDdSRzZWY1JJMzNDejhNWmFQUG1MSExQQWtkeHJCek5tUitGVFNzeWs9;MTQ3MDAwMkAzMjMxMmUzMTJlMzMzNU5FbmZvRTB3Mm9peXdBcHZOTEIwcnFyQm1QREJXMkZib3lYalhJRVFzMDQ9;MTQ3MDAwM0AzMjMxMmUzMTJlMzMzNU01NFY1WEN1SEZ0eUhIOFBWQmVqM3B5TlF0SFI5MzhmZ01oaExHWURrZ0E9");
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
