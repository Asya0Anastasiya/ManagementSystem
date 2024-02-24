using AutoMapper;
using FluentValidation;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Data;
using TimeTrackingService.Interfaces.Repositories;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Mappers;
using TimeTrackingService.MediatR;
using TimeTrackingService.Middleware;
using TimeTrackingService.Models.Validators;
using TimeTrackingService.Options;
using TimeTrackingService.Repositories;
using TimeTrackingService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DayAccountingValidator>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config => 
    config.RegisterServicesFromAssembly(typeof(Program).Assembly)
    .AddOpenBehavior(typeof(ValidationBehavior<,>)));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<IDayAccountingRepository, DayAccountingRepository>();
builder.Services.AddScoped<IDayAccountingService, DayAccountingService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddHostedService<Consumer>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
        builder.WithExposedHeaders("X-Pagination");
    });
});

builder.Services.AddDbContext<TimeTrackingContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));

var mappingConfig = new MapperConfiguration(x =>
{
    x.AddProfile(new AutoMappers());
});
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
