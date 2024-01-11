using AutoMapper;
using DocumentServiceApi.Data;
using DocumentServiceApi.Interfaces.Repositories;
using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Mappers;
using DocumentServiceApi.Repositiries;
using DocumentServiceApi.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using DocumentServiceApi.MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly)
    .AddOpenBehavior(typeof(ValidationBehavior<,>)));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

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

builder.Services.AddDbContext<DocumentContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

var mappingConfig = new MapperConfiguration(x =>
{
    x.AddProfile(new AutoMappers());
});
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
