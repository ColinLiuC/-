using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class PropertyConfig : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("T_Property").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.JuWeiId).IsRequired();
            builder.Property(i => i.QuartersId).IsRequired();
            builder.Property(i => i.AdminAddress).HasMaxLength(200);
            builder.Property(i => i.CompanyAddress).HasMaxLength(200); 
            builder.Property(i => i.ManagerName).HasMaxLength(50); 
            builder.Property(i => i.ManagerTel).HasMaxLength(50);
            builder.Property(i => i.GuaranteeTel).HasMaxLength(50);
            builder.Property(i => i.EnclosureName).HasMaxLength(500);
            builder.Property(i => i.EnclosureUrl).HasMaxLength(500);
            builder.Property(i => i.Remarks).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}

