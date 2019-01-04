using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class DoorCardConfig : IEntityTypeConfiguration<DoorCard>
    {
        public void Configure(EntityTypeBuilder<DoorCard> builder)
        {
            builder.ToTable("T_DoorCard").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.DoorCardNumber).HasMaxLength(100).IsRequired();
            builder.Property(i => i.BuildingId).IsRequired();
            builder.Property(i => i.ChargeUser).HasMaxLength(50);
            builder.Property(i => i.DangDaiBiaoUser).HasMaxLength(50);
            builder.Property(i => i.BuildLeaderUser).HasMaxLength(50);
            builder.Property(i => i.JuMingLeaderUser).HasMaxLength(50);
            builder.Property(i => i.Remarks).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
