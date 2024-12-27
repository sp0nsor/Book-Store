using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyFlow.DataAccess.Entities;

namespace MoneyFlow.DataAccess.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(a => a.AccountNumber);

            builder.Property(a => a.FirstName)
                .IsRequired();

            builder.Property(a => a.LastName)
                .IsRequired();

            builder.Property(a => a.SecretKeyHash)
                .IsRequired();

            builder.Property(a => a.MoneyAmount);
        }
    }
}
