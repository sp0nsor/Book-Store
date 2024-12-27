using Microsoft.AspNetCore.Http;
using MoneyFlow.Core.Models;

namespace MoneyFlow.Core.Services
{
    public interface IAccountService
    {
        Task<Guid> CreateAccount(Account account);
        Task<List<Account>> GetAccounts();
        Task MakeTranfer(Transfer transfer);
    }
}