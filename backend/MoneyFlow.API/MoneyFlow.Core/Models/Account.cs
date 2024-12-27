using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace MoneyFlow.Core.Models
{
    public class Account
    {
        private Account(Guid accountNumber, string secretKeyHash,
            string ownerFirstName, string ownerLastName, decimal moneyAmount)
        {
            AccountNumber = accountNumber;
            SecretKeyHash = secretKeyHash;
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            MoneyAmount = moneyAmount;
        }

        public Guid AccountNumber { get; private set; }
        public string SecretKeyHash { get; private set; }
        public string OwnerFirstName { get; private set; }
        public string OwnerLastName { get; private set; }
        public decimal MoneyAmount { get; private set; }

        public static(Account Account, string Error) Create(Guid accountNumber,
            string secretKeyHash, string ownerFirstName, string ownerLastName, decimal moneyAmount)
        {
            var error = string.Empty;

            if(string.IsNullOrEmpty(ownerFirstName)
                || string.IsNullOrEmpty(ownerLastName)
                || moneyAmount < 0)
            {
                error = "Incorrect account data!";
            }

            var account = new Account(
                accountNumber,
                secretKeyHash,
                ownerFirstName,
                ownerLastName,
                moneyAmount);

            return (account, error);
        }
    }
}
