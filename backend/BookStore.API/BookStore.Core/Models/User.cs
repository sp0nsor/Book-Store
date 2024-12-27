namespace BookStore.Core.Models
{
    public class User
    {
        private User(Guid id, string userName, string password, string email)
        {
            Id = id;
            UserName = userName;
            PasswordHash = password;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }

        public static User Create(Guid id, string userName, string password, string email)
        {
            return new User(id, userName, password, email);
        }
    }
}
