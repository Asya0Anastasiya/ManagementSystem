using AutoMapper;
using UserService.Data;
using UserService.Interfaces.Repositories;
using UserService.Interfaces.Services;
using UserService.Mappers;
using UserService.Middleware;
using UserService.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using UserService.MediatR;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly)
    //.AddOpenBehavior(typeof(ValidationBehavior<,>))
    );

//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<IUserService, UserService.Services.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        // почему принимает запросы с хоста оцелота, даже если стоит .WithOrigins("не_хост_оцелота")???????????
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
        builder.WithExposedHeaders("X-Pagination");
    });
}); 

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


