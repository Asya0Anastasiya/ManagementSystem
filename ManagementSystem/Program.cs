using AutoMapper;
using UserService.Data;
using UserService.Interfaces.Repositories;
using UserService.Interfaces.Services;
using UserService.Mappers;
using UserService.Middleware;
using UserService.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService.Services.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Host.UseSerilog();

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        // ������ ��������� ������� � ����� �������, ���� ���� ����� .WithOrigins("��_����_�������")???????????
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
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


