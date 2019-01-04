using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class StreetConfig : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.ToTable("T_Street").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.StreetName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StreetAddress).HasMaxLength(200);
            builder.Property(i => i.StreetPeople).HasMaxLength(50);
            builder.Property(i => i.StreetTell).HasMaxLength(50);
            builder.Property(i => i.StreetInfo).HasMaxLength(500);
            builder.Property(i => i.StreetImg).HasMaxLength(500);
            builder.Property(i => i.StreetPaths).HasMaxLength(500);
            builder.Property(i => i.Lat);
            builder.Property(i => i.Lng);

            builder.Property(i => i.StreetAttr1).HasMaxLength(200);
            builder.Property(i => i.StreetAttr2).HasMaxLength(200);
            builder.Property(i => i.StreetAttr3).HasMaxLength(200);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);

        }
    }
}
