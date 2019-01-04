using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PostStationVideoConfig : IEntityTypeConfiguration<PostStationVideo>
    {
        public void Configure(EntityTypeBuilder<PostStationVideo> builder)
        {
            builder.ToTable("T_Video").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.StreetName).HasMaxLength(50);
            builder.Property(i => i.PostStationId).IsRequired();
            builder.Property(i => i.PostStationName).HasMaxLength(50);
            builder.Property(i => i.ViedoImgPath).HasMaxLength(200);
            builder.Property(i => i.ViedoPath).HasMaxLength(200);
            builder.Property(i => i.ViedoName).HasMaxLength(50);
        }
    }
}
