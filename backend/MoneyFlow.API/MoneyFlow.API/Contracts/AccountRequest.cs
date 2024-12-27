namespace MoneyFlow.API.Contracts
{
    public record AccountRequest(
        string FirstName,
        string LastName,
        decimal MoneyAmount,
        string ScretKey);
}
