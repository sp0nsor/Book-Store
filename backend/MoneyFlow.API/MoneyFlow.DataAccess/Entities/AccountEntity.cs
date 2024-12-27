namespace MoneyFlow.DataAccess.Entities
{
    public class AccountEntity
    {
        public Guid AccountNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal MoneyAmount { get; set; } = 0;
        public string SecretKeyHash { get; set; } = string.Empty;
    }
}
