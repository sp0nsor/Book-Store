using Microsoft.AspNetCore.Http;
using MoneyFlow.Core.Models;
using MoneyFlow.Core.Services;
using MoneyFlow.DataAccess.Repositories;
using MoneyFlow.Infrastructure;

namespace MoneyFlow.AppLication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IKeyHasher keyHasher;

        public AccountService(IAccountRepository accountRepository, IKeyHasher keyHasher)
        {
            this.accountRepository = accountRepository;
            this.keyHasher = keyHasher;
        }

        public async Task<Guid> CreateAccount(Account account)
        {
            var result = await accountRepository.Create(account);

            return result;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var accounts = await accountRepository.Get();

            return accounts;
        }

        public async Task MakeTranfer(Transfer transfer)
        {
            var account = await accountRepository
                .GetByAccountNumber(transfer.SenderAccountNumber);

            var isTrueSender = keyHasher.Verify(
                transfer.SenderSecretKey,
                account.SecretKeyHash);

            if (!isTrueSender)
            {
                throw new Exception("Account data has not been verified");
            }

            if (transfer.MoneyAmount > account.MoneyAmount)
            {
                throw new Exception("There is not enough money in the account");
            }

            await accountRepository.MakeTransfer(
                transfer.SenderAccountNumber,
                transfer.RecipientAccountNumber,
                transfer.MoneyAmount);
        }
    }
}
