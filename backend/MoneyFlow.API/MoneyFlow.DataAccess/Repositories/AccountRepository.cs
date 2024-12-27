using Microsoft.EntityFrameworkCore;
using MoneyFlow.Core.Models;
using MoneyFlow.DataAccess.Entities;

namespace MoneyFlow.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MoneyFlowDbContext context;

        public AccountRepository(MoneyFlowDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Account>> Get()
        {
            var accountEntities = await context.Accounts
                .AsNoTracking()
                .ToListAsync();

            var accounts = accountEntities.Select(
                a => Account.Create(
                    a.AccountNumber,
                    a.SecretKeyHash,
                    a.FirstName,
                    a.LastName,
                    a.MoneyAmount).Account).ToList();

            return accounts;
        }

        public async Task<Guid> Create(Account accout)
        {
            var accoutEntity = new AccountEntity
            {
                AccountNumber = accout.AccountNumber,
                SecretKeyHash = accout.SecretKeyHash,
                FirstName = accout.OwnerFirstName,
                LastName = accout.OwnerLastName,
                MoneyAmount = accout.MoneyAmount,
            };

            await context.Accounts.AddAsync(accoutEntity);
            await context.SaveChangesAsync();

            return accout.AccountNumber;
        }

        public async Task<Account> GetByAccountNumber(Guid accountNumber)
        {
            var accountEntity = await context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber)
                ?? throw new Exception($"Account with number {accountNumber} was not found");

            var account = Account.Create(
                accountEntity.AccountNumber,
                accountEntity.SecretKeyHash,
                accountEntity.FirstName,
                accountEntity.LastName,
                accountEntity.MoneyAmount).Account;


            return account;
        }

        public async Task MakeTransfer(Guid senderNumber, Guid recipientNumber, decimal moneyAmount)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var senderAccountEntity = await context.Accounts
                    .FirstOrDefaultAsync(a => a.AccountNumber == senderNumber)
                    ?? throw new Exception($"Sender account with number {senderNumber} was not found");

                var recipientAccountEntity = await context.Accounts
                    .FirstOrDefaultAsync(a => a.AccountNumber == recipientNumber)
                    ?? throw new Exception($"Recipient account with number {recipientNumber} was not found");

                senderAccountEntity.MoneyAmount -= moneyAmount;
                recipientAccountEntity.MoneyAmount += moneyAmount;

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
