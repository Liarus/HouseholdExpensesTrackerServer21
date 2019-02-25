using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Context.Configurations
{
    public class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.ToTable("ExpenseTypes");

            builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(false);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property("_createdBy")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("CreatedBy");
            builder.Property("_createdDate")
                .IsRequired()
                .HasColumnName("CreatedDate");
            builder.Property("_updatedBy")
                .HasMaxLength(255)
                .HasColumnName("UpdatedBy");
            builder.Property("_updatedDate")
                .HasColumnName("UpdatedDate");
            builder.Property(e => e.SearchValue)
                .HasColumnName("SearchValue");
            builder.Property(e => e.Version)
                .IsConcurrencyToken()
                .HasColumnName("Version");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.Symbol)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
