using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PropertyRepairConfig : IEntityTypeConfiguration<PropertyRepair>
    {
        public void Configure(EntityTypeBuilder<PropertyRepair> builder)
        {
            builder.ToTable("T_PropertyRepair").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.RepairTime).IsRequired();
            builder.Property(i => i.RepairMatter).HasMaxLength(500).IsRequired();
            builder.Property(i => i.RepairAddress).HasMaxLength(200).IsRequired();
            builder.Property(i => i.RegisterUser).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RegisterTime).IsRequired();
            builder.Property(i => i.StatusCode).IsRequired();
            builder.Property(i => i.DisposeResult).HasMaxLength(500);
            builder.Property(i => i.DisposeUser);
            builder.Property(i => i.DisposeTime);

            builder.Property(i => i.ContactUser).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ContactTel).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.JuWeiId).IsRequired();
            builder.Property(i => i.QuartersId).IsRequired();
            builder.Property(i => i.PropertyId).IsRequired();
            builder.Property(i => i.Remarks).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
