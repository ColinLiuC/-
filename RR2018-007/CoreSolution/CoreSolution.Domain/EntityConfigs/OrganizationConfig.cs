using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class OrganizationConfig: IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("T_Organization").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.OrganizationName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.OrganizationType);
            builder.Property(i => i.IndustryCategory);
            builder.Property(i => i.Contacts).HasMaxLength(50);
            builder.Property(i => i.ContactNumber).HasMaxLength(50);
            builder.Property(i => i.Members);
            builder.Property(i => i.Address).HasMaxLength(500);
            builder.Property(i => i.DetailsIntroduction).HasMaxLength(500);
            builder.Property(i => i.AttachmentName);
            builder.Property(i => i.AttachmentPath);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
