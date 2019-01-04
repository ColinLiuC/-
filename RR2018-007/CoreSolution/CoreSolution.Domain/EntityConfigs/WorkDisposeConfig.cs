using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class WorkDisposeConfig: IEntityTypeConfiguration<WorkDispose>
    {
        public void Configure(EntityTypeBuilder<WorkDispose> builder)
        {
            builder.ToTable("T_WorkDispose").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ResidentWorkId).IsRequired();
            builder.Property(i => i.ShiMinYunId).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ResidentWorkName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.UserName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.IdCard).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Address).HasMaxLength(200);
            builder.Property(i => i.Phone).HasMaxLength(50);
            builder.Property(i => i.Remarks);
            builder.Property(i => i.StatusCode).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StreetId);
            builder.Property(i => i.StreetName).HasMaxLength(50);
            builder.Property(i => i.PostStationId);
            builder.Property(i => i.PostStationName).HasMaxLength(50);
            builder.Property(i => i.DisposeUser).HasMaxLength(50);
            builder.Property(i => i.DisposeResult);
            builder.Property(i => i.DisposeTime);

            builder.Property(i => i.YuYueTime);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }

    }
}
