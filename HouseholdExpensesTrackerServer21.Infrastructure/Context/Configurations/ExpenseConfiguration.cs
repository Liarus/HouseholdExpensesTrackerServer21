using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Context.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");

            builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(false);

            builder.HasOne<ExpenseType>()
                .WithMany()
                .HasForeignKey(e => e.ExpenseTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Household>()
                .WithMany()
                .HasForeignKey(e => e.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

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
            builder.Property(e => e.Version)
                .IsConcurrencyToken()
                .HasColumnName("Version");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.Description)
                .HasMaxLength(255);
            builder.Property(e => e.Date)
                .IsRequired();
            builder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("money");
            builder.OwnsOne(e => e.Period)
                .Property(e => e.PeriodEnd)
                .IsRequired();
            builder.OwnsOne(e => e.Period)
                .Property(e => e.PeriodStart)
                .IsRequired();
        }
    }
}
