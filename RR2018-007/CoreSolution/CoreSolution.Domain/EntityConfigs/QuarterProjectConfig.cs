using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class QuarterProjectConfig : IEntityTypeConfiguration<QuarterProject>
    {
        public void Configure(EntityTypeBuilder<QuarterProject> builder)
        {
            builder.ToTable("T_QuarterProject").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ProjectType).IsRequired();
            builder.Property(i => i.DeclareTime).IsRequired(); 
            builder.Property(i => i.Exploiting).HasMaxLength(100);
            builder.Property(i => i.ExploitingTime_Start);
            builder.Property(i => i.ExploitingTime_End);

            builder.Property(i => i.ContactUser).HasMaxLength(50);
            builder.Property(i => i.ContactTel).HasMaxLength(50);
            builder.Property(i => i.ProjectDetail).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
