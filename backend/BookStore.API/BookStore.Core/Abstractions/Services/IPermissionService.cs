using BookStore.Core.Enums;

namespace BookStore.Application.Services
{
    public interface IPermissionService
    {
        Task<ICollection<Permission>> GetPermissions(Guid userId);
    }
}