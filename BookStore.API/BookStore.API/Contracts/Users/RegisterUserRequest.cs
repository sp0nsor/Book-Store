using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Email,
        [Required] string Password);
}
