using BankingWallet.Application.DTOs;
using BankingWallet.Application.Services;
using BankingWallet.Domain.ValueObjects;
using BankingWallet.Application;
using BankingWallet.Infrastructure.DependencyInjection;
using BankingWallet.API.Endpoints;
using BankingWallet.API.ErrorHandling;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapWalletEndpoints();

app.Run();
