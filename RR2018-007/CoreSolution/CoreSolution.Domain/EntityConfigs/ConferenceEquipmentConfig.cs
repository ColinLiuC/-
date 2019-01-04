using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class ConferenceEquipmentConfig: IEntityTypeConfiguration<ConferenceEquipment>
    {
        public void Configure(EntityTypeBuilder<ConferenceEquipment> builder)
        {
            builder.ToTable("T_ConferenceEquipment").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.EquipmentName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Count);
            builder.Property(i => i.Description).HasMaxLength(1000);
            builder.Property(i => i.ConferenceRoomId).IsRequired();
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
