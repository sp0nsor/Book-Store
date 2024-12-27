using BookStore.Core.Abstractions.Auth;
using BookStore.Core.Abstractions.Repositories;
using BookStore.Core.Abstractions.Services;
using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IPasswordHasher hasher,
            IUsersRepository usersRepository,
            IJwtProvider jwtProvider)
        {
            _hasher = hasher;
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string name, string email, string password)
        {
            var passwordHash = _hasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), name, passwordHash, email);

            await _usersRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _hasher.Verify(password, user.PasswordHash);

            if (!result)
                throw new Exception("Fail to login");

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
