using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CoreSolution.Domain.EntityConfigs
{

    public class WorkforceManagementConfig : IEntityTypeConfiguration<WorkforceManagement>
    {
        public void Configure(EntityTypeBuilder<WorkforceManagement> builder)
        {
            builder.ToTable("T_WorkforceManagement").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.StreetId);
            builder.Property(i => i.StationId);
            builder.Property(i => i.WorkforceYear).HasMaxLength(20);
            builder.Property(i => i.WorkforceMonth).HasMaxLength(20);
            builder.Property(i => i.WorkforceDay).HasMaxLength(20);
            builder.Property(i => i.WorkforceWeek).HasMaxLength(50);
            builder.Property(i => i.DayState);
            builder.Property(i => i.BeginTime);
            builder.Property(i => i.EndTime);
            builder.Property(i => i.PeopleGroupId);
            builder.Property(i => i.Remark).HasMaxLength(1000);
            builder.Property(i => i.RegisterPeople).HasMaxLength(50);
            builder.Property(i => i.RegisterDate);


            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
