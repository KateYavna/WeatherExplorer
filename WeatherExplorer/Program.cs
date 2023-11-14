using Microsoft.EntityFrameworkCore;
using WeatherData;
using WeatherData.Repositories;
using WeatherData.Repositories.Interfaces;
using WeatherServices.Services;
using WeatherServices.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();

var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(cs), ServiceLifetime.Scoped);

builder.Services.AddScoped<ICountryCodeRepository,CountryCodeRepository>();
builder.Services.AddScoped<IWeatherScreenRepository,WeatherScreenRepository>();
builder.Services.AddHostedService<WeatherRefreshService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();