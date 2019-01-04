using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
  public class ProjectServiceRecordConfig: IEntityTypeConfiguration<ProjectServiceRecord>
    {
        public void Configure(EntityTypeBuilder<ProjectServiceRecord> builder)
        {
            builder.ToTable("T_ProjectServiceRecord").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);           
            builder.Property(i => i.ProjectId);
            builder.Property(i => i.ServicePlace).HasMaxLength(500);
            builder.Property(i => i.ServiceDate);
            builder.Property(i => i.ServiceType);
            builder.Property(i => i.ChargePerson);
            builder.Property(i => i.ContactNumber);
            builder.Property(i => i.ServiceNumber);          
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
