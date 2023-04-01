using Microsoft.EntityFrameworkCore;
using Rastreador_de_Gastos.Models;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5gSHxTdkNiUH9eeHA=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1Qd0djWXlfeXRXRmQ=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdURhWX9YcX1VRWNY;MTU3NzM4NkAzMjMxMmUzMTJlMzMzNUQxeEVINnpGYytyODcwaWFhNkFWYzZybEQydjBUYzdSUlNCSzFJK0xoUWs9;MTU3NzM4N0AzMjMxMmUzMTJlMzMzNWhaR2Q1dnhLQy9RT3VsRHBBTjFERmVhUmhESVc1a3dGNU96Y1hjNlA4Y3M9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUZiW39ednVcRmlVVQ==;MTU3NzM4OUAzMjMxMmUzMTJlMzMzNVNHekovZ2NBQ0RzRy9NNHB3cWl3eXdQZWhva3JocG9ZVXRzRlh6UHJTcTg9;MTU3NzM5MEAzMjMxMmUzMTJlMzMzNVQyNnZjUkhYaUZuRlRNNWFraUlLQlNoUWtzWDBJaVF5Z1E0TldHemtySXc9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdURhWX9YcX1UQWFY;MTU3NzM5MkAzMjMxMmUzMTJlMzMzNVVnNUxBUis4eUpxZUdITTRuR3NTMm1NSENZUHZuZjQwL3JKWjJFcEJxMFU9;MTU3NzM5M0AzMjMxMmUzMTJlMzMzNWJDNHZhMDZybjR1ZklQdTdYaGRub1BSMkFsWnRtN0ZtS2o3ZkI1YTJYNnM9;MTU3NzM5NEAzMjMxMmUzMTJlMzMzNVNHekovZ2NBQ0RzRy9NNHB3cWl3eXdQZWhva3JocG9ZVXRzRlh6UHJTcTg9");
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
	pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
