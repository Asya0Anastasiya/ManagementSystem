using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Mappers;
using TimeTrackingService.Repositories;
using TimeTrackingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDaysAccountingRepository, DaysAccountingRepository>();
builder.Services.AddScoped<IDaysAccountingService, DaysAccountingService>();

builder.Services.AddDbContext<TimeTrackingContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

var mappingConfig = new MapperConfiguration(x =>
{
    x.AddProfile(new AutoMappers());
});
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
