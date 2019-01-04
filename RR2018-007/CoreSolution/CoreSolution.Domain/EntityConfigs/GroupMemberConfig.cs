using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{

    public class GroupMemberConfig : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.ToTable("T_GroupMember").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.PeopleGroupId);
            builder.Property(i => i.WorkPersonId);
            builder.Property(i => i.WorkPersonName).HasMaxLength(50);
            builder.Property(i => i.StreetId);
            builder.Property(i => i.StationId);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
