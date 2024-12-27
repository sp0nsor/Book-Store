using BookStore.Core.Enums;
using BookStore.Core.Models;

namespace BookStore.Core.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<ICollection<Permission>> GetUserPermissions(Guid userId);
    }
}