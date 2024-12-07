using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Enums;
using BookStore.Core.Models;
using BookStore.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BookStoreDbContext _context;

        public UsersRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin)
                ?? throw new InvalidOperationException();

            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Roles = [roleEntity]
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            var user = User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);

            return user;
        }

        public async Task<ICollection<Permission>> GetUserPermissions(Guid userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            Console.WriteLine(roles[0].First().Permissions.First().Name);

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToList();
        }

    }
}
