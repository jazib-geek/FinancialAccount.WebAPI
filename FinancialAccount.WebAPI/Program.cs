using Application;
using D2Soft.Application.Abstractions;
using D2Soft.Application.FinancialAccounts.Commands;
using D2Soft.Application.FinancialAccounts.Queries;
using D2Soft.Application.FinancialAccounts.QueryHandlers;
using D2Soft.Infrastructure;
using D2Soft.Infrastructure.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.AddConsole();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application and Insfrastucture Services
builder.Services
    .AddApplication()
    .AddInfrastructure();

// Repos
builder.Services.AddScoped<IFinancialAccountRepository, FinancialAccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddMediatR(typeof(GetFinancialAccountById));

//builder.Services.AddMediatR(typeof(GetFinancialAccountById).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Connection string
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs)); ;

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
