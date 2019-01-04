using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PeopleAndActivityConfig : IEntityTypeConfiguration<PeopleAndActivity>
    {
        public void Configure(EntityTypeBuilder<PeopleAndActivity> builder)
        {
            builder.ToTable("T_PeopleAndActivitys").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.PeopleID).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ActivityID).HasMaxLength(50).IsRequired();
            builder.Property(i => i.IsComment).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Comment).HasMaxLength(500);
            builder.Property(i => i.Satisfaction);

            builder.Property(i => i.IsDeleted).IsRequired();
        }
    }
}
