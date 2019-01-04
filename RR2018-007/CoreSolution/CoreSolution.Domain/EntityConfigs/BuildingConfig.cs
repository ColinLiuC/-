using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class BuildingConfig : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("T_Building").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Address).HasMaxLength(500).IsRequired();
            builder.Property(i => i.QuartersId).IsRequired();
            builder.Property(i => i.IsElevator).IsRequired();
            builder.Property(i => i.Property).HasMaxLength(100);
            builder.Property(i => i.Phone).HasMaxLength(50);
            builder.Property(i => i.Remarks).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
