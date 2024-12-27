namespace MoneyFlow.Core.Models
{
    public class Transfer
    {
        private Transfer(Guid senderAccountNumber, Guid recipientAccountNumber,
            string senderSecretKey, decimal moneyAmount)
        {
            SenderAccountNumber = senderAccountNumber;
            RecipientAccountNumber = recipientAccountNumber;
            SenderSecretKey = senderSecretKey;
            MoneyAmount = moneyAmount;
        }

        public Guid SenderAccountNumber { get; private set; }
        public Guid RecipientAccountNumber { get; private set; }
        public string SenderSecretKey { get; private set; }
        public decimal MoneyAmount { get; private set; }

        public static(Transfer Transfer, string Error) Create(
            Guid senderAccountNumber, Guid recipientAccountNumber,
            string senderSecretKey,  decimal moneyAmount)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(senderSecretKey))
            {
                error = "Secret key can not be empty";
            }

            if (moneyAmount <= 0)
            {
                error = "Money can not be nagative or equal 0";
            }

            var transfer = new Transfer(
                senderAccountNumber,
                recipientAccountNumber,
                senderSecretKey,
                moneyAmount);

            return (transfer, error);
        }
    }
}
