using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ParkingLotConfig : IEntityTypeConfiguration<ParkingLot>
    {
        public void Configure(EntityTypeBuilder<ParkingLot> builder)
        {
            builder.ToTable("T_ParkingLot").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.StreetName).HasMaxLength(50);
            builder.Property(i => i.JuWeiId).IsRequired();
            builder.Property(i => i.JuWeiName).HasMaxLength(50);
            builder.Property(i => i.QuartersId).IsRequired();
            builder.Property(i => i.QuartersName).HasMaxLength(50);

            builder.Property(i => i.ParkingCount).IsRequired();
            builder.Property(i => i.ChanQuanCount);
            builder.Property(i => i.PublicCount);
            builder.Property(i => i.PublicChargeCount);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }

    }
}
