﻿using BookStore.Core.Enums;
using BookStore.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations
{
    public partial class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(p => p.Id);

            var permissions = Enum
                .GetValues<Permission>()
                .Select(p => new PermissionEntity
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            builder.HasData(permissions);
        }
    }
}
