using BookStore.Application.Services;
using BookStore.API.Contracts.Users;

namespace BookStore.API.Endpoints
{
    public static class UsersEndpoint
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Login(
            LoginUserRequest request, UsersService usersService, HttpContext httpContext)
        {
            var token = await usersService.Login(request.Email, request.Password);

            httpContext.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok();
        }

        private static async Task<IResult> Register
            (RegisterUserRequest request, UsersService usersService)
        {
            await usersService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }
    }
}
