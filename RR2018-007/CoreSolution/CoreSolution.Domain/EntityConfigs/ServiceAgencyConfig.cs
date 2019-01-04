using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class ServiceAgencyConfig : IEntityTypeConfiguration<ServiceAgency>
    {
        public void Configure(EntityTypeBuilder<ServiceAgency> builder)
        {
            builder.ToTable("T_ServiceAgency").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.AgencyName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.AgencyCategory);
            builder.Property(i => i.AgencyLeader).HasMaxLength(100);
            builder.Property(i => i.ContactPhone).HasMaxLength(50);
            builder.Property(i => i.ContactAddress).HasMaxLength(500);
            builder.Property(i => i.Description).HasMaxLength(500);
            builder.Property(i => i.SaImgName);
            builder.Property(i => i.SaImgPath);
            builder.Property(i => i.QualificationsName);
            builder.Property(i => i.QualificationsPath);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
