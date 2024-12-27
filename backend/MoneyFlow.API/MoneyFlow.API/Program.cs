using Microsoft.EntityFrameworkCore;
using MoneyFlow.API.Endpoints;
using MoneyFlow.AppLication.Services;
using MoneyFlow.Core.Services;
using MoneyFlow.DataAccess;
using MoneyFlow.DataAccess.Repositories;
using MoneyFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IKeyHasher, KeyHasher>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoneyFlowDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MoneyFlowDbContext)));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAccountEndpoints();

app.Run();
