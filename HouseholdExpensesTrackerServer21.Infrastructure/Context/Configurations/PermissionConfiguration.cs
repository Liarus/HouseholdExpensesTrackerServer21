using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Context.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(false);

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
                .IsRowVersion()
                .HasColumnName("Version");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.Name)
                .HasMaxLength(255);
        }
    }
}
