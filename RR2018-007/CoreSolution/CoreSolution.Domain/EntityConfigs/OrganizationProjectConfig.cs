using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class OrganizationProjectConfig : IEntityTypeConfiguration<OrganizationProject>
    {
        public void Configure(EntityTypeBuilder<OrganizationProject> builder)
        {
            builder.ToTable("T_OrganizationProject").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ProjectName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ProjectCategory);
            builder.Property(i => i.Client).HasMaxLength(100);
            builder.Property(i => i.TargetGroup).HasMaxLength(100);
            builder.Property(i => i.PrimaryCoverage).HasMaxLength(500);
            builder.Property(i => i.ImplementationTime);
            builder.Property(i => i.ProjectFunds);
            builder.Property(i => i.SourceFunds);
            builder.Property(i => i.Remarks).HasMaxLength(500);
            builder.Property(i => i.Registrant);
            builder.Property(i => i.RegistrationDate);
            builder.Property(i => i.AttachmentName);
            builder.Property(i => i.AttachmentPath);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
