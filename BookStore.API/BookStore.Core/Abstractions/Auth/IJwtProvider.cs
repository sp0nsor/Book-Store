using BookStore.Core.Models;

namespace BookStore.Core.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}