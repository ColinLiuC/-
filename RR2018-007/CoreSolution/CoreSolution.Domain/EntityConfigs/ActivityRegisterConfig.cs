using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ActivityRegisterConfig : IEntityTypeConfiguration<ActivityRegister>
    {
        public void Configure(EntityTypeBuilder<ActivityRegister> builder)
        {
            builder.ToTable("T_ActivityRegister").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.EnrolmentName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ActivityId).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ContactNumber).HasMaxLength(50);
            builder.Property(i => i.IsComment).HasMaxLength(50);
            builder.Property(i => i.Comment).HasMaxLength(200);
            builder.Property(i => i.Satisfaction).HasMaxLength(50);
        }

    }
}
