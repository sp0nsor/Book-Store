namespace BookStore.Core.Abstractions.Services
{
    public interface IUsersService
    {
        Task<string> Login(string email, string password);
        Task Register(string name, string email, string password);
    }
}