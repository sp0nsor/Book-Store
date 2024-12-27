using Microsoft.EntityFrameworkCore;
using MoneyFlow.DataAccess.Entities;

namespace MoneyFlow.DataAccess
{
    public class MoneyFlowDbContext : DbContext
    {
        public MoneyFlowDbContext(DbContextOptions<MoneyFlowDbContext> options)
            :base(options) { }

        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoneyFlowDbContext).Assembly);
        }
    }
}
