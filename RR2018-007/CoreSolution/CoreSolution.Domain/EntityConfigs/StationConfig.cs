using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSolution.Domain.EntityConfigs
{
    public class StationConfig:IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.ToTable("T_Stations").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.StationName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StationAddress).HasMaxLength(200).IsRequired();
            builder.Property(i => i.StationPeople).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StationTell).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StationTime).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StationInfo).HasMaxLength(500).IsRequired();
            builder.Property(i => i.StationImg).HasMaxLength(100);
            builder.Property(i => i.StationSrc).HasMaxLength(500);
            builder.Property(i => i.StreetID).IsRequired();
            builder.Property(i => i.StreetName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.StationType).HasMaxLength(50);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
            builder.Property(i => i.Sort);

        }
    }
}
