namespace BookStore.API.Contracts
{
    public record TransferRequest(
        Guid SenderAccountNumber,
        string SenderSecretKey,
        Guid RecipientAccountNumber,
        decimal moneyAmount);
}
