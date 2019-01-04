
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class MapOverlaysConfig : IEntityTypeConfiguration<MapOverlays>
    {
        public void Configure(EntityTypeBuilder<MapOverlays> builder)
        {
            builder.ToTable("T_MapOverlays").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.MapCenter);
            builder.Property(i => i.Longitude);
            builder.Property(i => i.Latitude);
            builder.Property(i => i.Type);
           
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }



    }
}

