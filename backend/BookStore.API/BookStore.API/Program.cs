using BookStore.API.Endpoints;
using BookStore.API.Extensions;
using BookStore.Application.Services;
using BookStore.Core.Abstractions.Auth;
using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Abstractions.Services;
using BookStore.Core.Enums;
using BookStore.DataAccess;
using BookStore.DataAccess.Repositories;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection((nameof(JwtOptions))));
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection((nameof(AuthorizationOptions))));

builder.Services.AddApiAuthentication(
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>()
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBooksService, BooksService>();

builder.Services.AddDbContext<BookStoreDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(BookStoreDbContext)));
    });

builder.Services.AddHttpClient<IPaymentService, PaymentService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5260");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});*/

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBooksEndpoints();
app.MapUsersEndpoints();

app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:3000")
          .AllowCredentials()
          .AllowAnyHeader()
          .AllowAnyMethod();
});

app.Run();
