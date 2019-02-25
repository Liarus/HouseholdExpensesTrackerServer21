using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(false);

            var navigation = builder.Metadata
                .FindNavigation(nameof(User.Credentials));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            navigation = builder.Metadata
                .FindNavigation(nameof(User.UserRoles));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);


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

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
