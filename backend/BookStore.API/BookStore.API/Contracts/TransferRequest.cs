using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Contracts
{
    public record TransferRequest(
        [Required] Guid SenderAccountNumber,
        [Required] string SenderSecretKey,
        [Required] decimal BookPrice);
}
