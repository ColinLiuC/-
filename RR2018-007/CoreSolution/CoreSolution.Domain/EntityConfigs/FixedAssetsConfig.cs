using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class FixedAssetsConfig : IEntityTypeConfiguration<FixedAssets>
    {
        public void Configure(EntityTypeBuilder<FixedAssets> builder)
        {
            builder.ToTable("T_FixedAssets").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Number).HasMaxLength(50);
            builder.Property(i => i.Name).HasMaxLength(50);
            builder.Property(i => i.Version).HasMaxLength(50);
            builder.Property(i => i.CurrentState);
            builder.Property(i => i.DutyPeople).HasMaxLength(50);
            builder.Property(i => i.Telephone).HasMaxLength(50);
            builder.Property(i => i.PurchaseDate);
            builder.Property(i => i.Description);
            builder.Property(i => i.Photo).HasMaxLength(3000);
            builder.Property(i => i.PhotoUrl).HasMaxLength(3000);
            builder.Property(i => i.RegisterPeople).HasMaxLength(50);
            builder.Property(i => i.RegisterDate);
            builder.Property(i => i.StreetId);
            builder.Property(i => i.StationId);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
          
        }
    }
}
