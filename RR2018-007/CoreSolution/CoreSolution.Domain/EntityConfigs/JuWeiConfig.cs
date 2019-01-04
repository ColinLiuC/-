using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class JuWeiConfig : IEntityTypeConfiguration<JuWei>
    {
        public void Configure(EntityTypeBuilder<JuWei> builder)
        {
            builder.ToTable("T_JuWei").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Address).HasMaxLength(500).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.StreetName).HasMaxLength(50);

            builder.Property(i => i.PostStationId).IsRequired();
            builder.Property(i => i.PostStationName).HasMaxLength(50);

            builder.Property(i => i.JuWeiPeople).HasMaxLength(50);
            builder.Property(i => i.Phone).HasMaxLength(50);
            builder.Property(i => i.Introduce).HasMaxLength(500);
            builder.Property(i => i.Lat);
            builder.Property(i => i.Lng);


            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
