using AutoMapper;
using UserService.Data;
using UserService.Interfaces.Repositories;
using UserService.Interfaces.Services;
using UserService.Mappers;
using UserService.Middleware;
using UserService.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using UserService.Models.Validators;
using UserService.Helpers;
using MediatR;
using UserService.MediatR;
using FluentValidation;
using UserService.Services;
using UserService.Hubs;
using UserService.Models.Options;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly)
    .AddOpenBehavior(typeof(ValidationBehavior<,>))
    );

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserService, UserService.Services.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddressValidator>());

builder.Services.Configure <RefreshTokenOptions>(builder.Configuration.GetSection("RefreshToken"));

builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailOptions"));

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowCredentials()
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
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

app.Run();