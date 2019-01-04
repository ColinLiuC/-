using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class WorkTransactConfig : IEntityTypeConfiguration<WorkTransact>
    {
        public void Configure(EntityTypeBuilder<WorkTransact> builder)
        {
            builder.ToTable("T_WorkTransact").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ResidentWorkId).IsRequired();
            builder.Property(i => i.ResidentWorkName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.UserName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.IdCard).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Address).HasMaxLength(200);
            builder.Property(i => i.Phone).HasMaxLength(50);   
            builder.Property(i => i.StatusCode).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Remarks).HasMaxLength(500);
            builder.Property(i => i.PeopleID);

            builder.Property(i => i.ShouliUser).HasMaxLength(50);
            builder.Property(i => i.ShouliContent).HasMaxLength(500);
            builder.Property(i => i.ShouliAddress).HasMaxLength(200);
            builder.Property(i => i.ShouliTime);

            builder.Property(i => i.DisposeUser).HasMaxLength(50);
            builder.Property(i => i.DisposeResult).HasMaxLength(500);
            builder.Property(i => i.DisposeTime);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
