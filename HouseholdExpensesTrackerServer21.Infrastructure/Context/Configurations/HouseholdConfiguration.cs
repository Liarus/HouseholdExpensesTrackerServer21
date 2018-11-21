using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Context.Configurations
{
    public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
    {
        public void Configure(EntityTypeBuilder<Household> builder)
        {
            builder.ToTable("Households");

            builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(false);

            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(e => e.UserId)
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
            builder.OwnsOne(e => e.Address)
                .Property(e => e.City)
                .IsRequired()
                .HasMaxLength(255);
            builder.OwnsOne(e => e.Address)
                .Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(255);
            builder.OwnsOne(e => e.Address)
                .Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(255);
            builder.OwnsOne(e => e.Address)
                .Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
