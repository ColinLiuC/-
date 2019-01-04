using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class ActivityCheckInConfig: IEntityTypeConfiguration<ActivityCheckIn>
    {
        public void Configure(EntityTypeBuilder<ActivityCheckIn> builder)
        {
            builder.ToTable("T_ActivityCheckIn").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.EnrolmentName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ContactNumber);
            builder.Property(i => i.ActivityId);
            builder.Property(i => i.SignUpDate);           
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
