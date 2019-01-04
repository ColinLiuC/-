using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class HouseConfig : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.ToTable("T_House").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.HouseNumber).HasMaxLength(100).IsRequired();
            builder.Property(i => i.DoorCardId).IsRequired();
            builder.Property(i => i.OrientationId);
            builder.Property(i => i.BuildArea);
            builder.Property(i => i.Remarks).HasMaxLength(500);
            builder.Property(i => i.BenShiUserCount);
            builder.Property(i => i.WaiLaiUserCount);


            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
