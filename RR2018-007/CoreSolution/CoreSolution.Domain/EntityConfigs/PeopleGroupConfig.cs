using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CoreSolution.Domain.EntityConfigs
{
    public class PeopleGroupConfig : IEntityTypeConfiguration<PeopleGroup>
    {
        public void Configure(EntityTypeBuilder<PeopleGroup> builder)
        {
            builder.ToTable("T_PeopleGroup").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.StreetId );
            builder.Property(i => i.StationId );
            builder.Property(i => i.GroupName).HasMaxLength(300);
            builder.Property(i => i.GroupCateGory);
            builder.Property(i => i.DutyPeople).HasMaxLength(50);
            builder.Property(i => i.DutyPeopleTelPhone).HasMaxLength(50);
            builder.Property(i => i.Remark).HasMaxLength(1000);
            builder.Property(i => i.WorkPersonIds);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
