﻿using Akb.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Akb.Data.Entity
{
    public class AccountTransaction : BaseEntityWithId
    {
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public string ReferenceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string TransferType { get; set; }




    }
    public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
    {
        public void Configure(EntityTypeBuilder<AccountTransaction> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.AccountId).IsRequired(true);
            builder.Property(x => x.TransactionDate).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 4);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.TransferType).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.ReferenceNumber).IsRequired(true).HasMaxLength(50);

            builder.HasIndex(x => x.ReferenceNumber);

        }


    }
}
