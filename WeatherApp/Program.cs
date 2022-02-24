using Microsoft.EntityFrameworkCore;
using WeatherApp.Extensions;
using WeatherApp.BusinessLogic;
using WeatherApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IWeatherDbContext, WeatherDbContext>(options => options.UseInMemoryDatabase("InMemoryTestDb")); ;
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<IWeatherLogic, WeatherLogic>();


var app = builder.Build();

app.SeedWeatherData();

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
