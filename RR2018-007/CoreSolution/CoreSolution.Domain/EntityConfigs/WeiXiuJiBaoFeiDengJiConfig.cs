using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class WeiXiuJiBaoFeiDengJiConfig : IEntityTypeConfiguration<WeiXiuJiBaoFeiDengJi>
    {
        public void Configure(EntityTypeBuilder<WeiXiuJiBaoFeiDengJi> builder)
        {
            builder.ToTable("T_WeiXiuJiBaoFeiDengJi").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FixedAssetsId);
            builder.Property(i => i.Category);
            builder.Property(i => i.HappenDate);
            builder.Property(i => i.CurrentState);
            builder.Property(i => i.FinishDate);
            builder.Property(i => i.RegisterPeople).HasMaxLength(50);
            builder.Property(i => i.RegisterDate);
            builder.Property(i => i.Remark);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
