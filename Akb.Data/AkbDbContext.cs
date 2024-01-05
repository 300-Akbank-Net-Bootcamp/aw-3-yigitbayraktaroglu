﻿using Akb.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Akb.Data
{
    public class AkbDbContext : DbContext
    {
        public AkbDbContext(DbContextOptions<AkbDbContext> options) : base(options)
        {

        }

        //dbset
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EftTransaction> EftTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EftTransactionConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
