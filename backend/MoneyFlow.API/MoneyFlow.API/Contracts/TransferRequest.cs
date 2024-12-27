namespace MoneyFlow.API.Contracts
{
    public record TransferRequest(
        Guid SenderAccountNumber,
        string SenderSecretKey,
        decimal MoneyAmount,
        Guid RecipientAccountNumber);
}
