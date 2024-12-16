using Microsoft.EntityFrameworkCore;
using CacheApiTest.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RedisTestAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RedisTestAppContext") ?? throw new InvalidOperationException("Connection string 'RedisTestAppContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "RedisTestApp_";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
