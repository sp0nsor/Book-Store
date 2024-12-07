using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Enums;

namespace BookStore.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUsersRepository _usersRepository;

        public PermissionService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<ICollection<Permission>> GetPermissions(Guid userId)
        {
            return _usersRepository.GetUserPermissions(userId);
        }
    }
}
