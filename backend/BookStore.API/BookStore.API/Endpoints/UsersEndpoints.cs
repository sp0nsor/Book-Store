using BookStore.API.Contracts.Users;
using BookStore.Core.Abstractions.Services;

namespace BookStore.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("Register", Register);
            app.MapPost("Login", Login);

            return app;
        }

        private static async Task<IResult> Login(
            LoginUserRequest request,
            IUsersService usersService,
            HttpContext httpContext)
        {
            var token = await usersService.Login(request.Email, request.Password);

            httpContext.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok();
        }

        private static async Task<IResult> Register
            (RegisterUserRequest request, IUsersService usersService)
        {
            await usersService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }
    }
}
