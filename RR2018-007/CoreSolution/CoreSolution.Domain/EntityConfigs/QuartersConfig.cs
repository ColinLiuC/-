using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class QuartersConfig : IEntityTypeConfiguration<Quarters>
    {
        public void Configure(EntityTypeBuilder<Quarters> builder)
        {
            builder.ToTable("T_Quarters").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Address).HasMaxLength(200).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.StreetName).HasMaxLength(50);
            builder.Property(i => i.JuWeiId).IsRequired();
            builder.Property(i => i.JuWeiName).HasMaxLength(50);
            builder.Property(i => i.CompletedYear).HasMaxLength(50);

            builder.Property(i => i.UserCount).IsRequired();
            builder.Property(i => i.HujiCount);
            builder.Property(i => i.JuZhuCount);
            builder.Property(i => i.CityUserCount);
            builder.Property(i => i.CityManUserCount);
            builder.Property(i => i.CityGirlUserCount);
            builder.Property(i => i.ForeignUserCount);
            builder.Property(i => i.ForeignManUserCount);
            builder.Property(i => i.ForeignGirlUserCount);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
