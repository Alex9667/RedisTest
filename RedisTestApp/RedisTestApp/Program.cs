using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedisTestApp.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RedisTestAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RedisTestAppContext") ?? throw new InvalidOperationException("Connection string 'RedisTestAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<Seeder>();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "RedisTestApp_"; 
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    await seeder.Seed();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
