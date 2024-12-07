﻿using BookStore.DataAccess.Configurations;
using BookStore.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStore.DataAccess
{
    public class BookStoreDbContext : DbContext
    {
        private readonly AuthorizationOptions _authOptions;

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options,
            IOptions<AuthorizationOptions> authOptions)
            : base(options)
        {
            _authOptions = authOptions.Value;
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions));
        }
    }
}
