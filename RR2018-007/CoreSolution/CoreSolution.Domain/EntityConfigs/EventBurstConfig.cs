using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class EventBurstConfig : IEntityTypeConfiguration<EventBurst>
    {
        public void Configure(EntityTypeBuilder<EventBurst> builder)
        {
            builder.ToTable("T_EventBurst").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Address).HasMaxLength(200).IsRequired();
            builder.Property(i => i.HappenTime).IsRequired();
            builder.Property(i => i.EventDetails).HasMaxLength(500).IsRequired();
            builder.Property(i => i.DisposeDetails).HasMaxLength(500).IsRequired();

            builder.Property(i => i.RegisterUser).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RegisterTime).IsRequired();
      
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
