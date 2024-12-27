using MoneyFlow.Core.Models;

namespace MoneyFlow.DataAccess.Repositories
{
    public interface IAccountRepository
    {
        Task<Guid> Create(Account accout);
        Task<List<Account>> Get();
        Task<Account> GetByAccountNumber(Guid accountNumber);
        Task MakeTransfer(Guid senderNumber, Guid recipientNumber, decimal moneyAmount);
    }
}