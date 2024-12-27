namespace BookStore.Core.Models
{
    public class Transfer
    {
        private readonly Guid bookStoreAccountNumber = Guid.Parse("574FDDD8-671C-4C25-B2CA-BCE107D1E8A8");

        private Transfer(Guid senderAccountNumber, string senderSecretKey, decimal moneyAmount)
        {
            RecipientAccountNumber = bookStoreAccountNumber;
            SenderAccountNumber = senderAccountNumber;
            SenderSecretKey = senderSecretKey;
            MoneyAmount = moneyAmount;
        }

        public Guid SenderAccountNumber { get; private set; }
        public Guid RecipientAccountNumber { get; private set; }
        public string SenderSecretKey { get; private set; }
        public decimal MoneyAmount { get; private set; }

        public static(Transfer Transfer, string Error) Create(
            Guid senderAccountNumber, string senderSecretKey, decimal moneyAmount)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(senderSecretKey))
            {
                error = "Secret key can not be empty";
            }

            if(moneyAmount <=  0)
            {
                error = "Money can not be nagative or equal 0";
            }

            var transfer = new Transfer(
                senderAccountNumber,
                senderSecretKey,
                moneyAmount);

            return (transfer, error);
        }

    }
}
