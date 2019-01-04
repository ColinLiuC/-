using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class PropertyServeConfig : IEntityTypeConfiguration<PropertyServe>
    {
        public void Configure(EntityTypeBuilder<PropertyServe> builder)
        {
            builder.ToTable("T_PropertyServe").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.PropertyId).IsRequired();
            builder.Property(i => i.ChargeSituation).HasMaxLength(500);
            builder.Property(i => i.CostAmount);
            builder.Property(i => i.ChargeSituationId).IsRequired();


            builder.Property(i => i.ServeContent).HasMaxLength(500).IsRequired();
            builder.Property(i => i.ServeTel).HasMaxLength(50).IsRequired();

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
