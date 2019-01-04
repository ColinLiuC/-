using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class KarmaConfig : IEntityTypeConfiguration<Karma>
    {
        public void Configure(EntityTypeBuilder<Karma> builder)
        {
            builder.ToTable("T_Karma").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ContactUser).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ContactTel).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RenQiId).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.JuWeiId).IsRequired();
            builder.Property(i => i.QuartersId).IsRequired();
            builder.Property(i => i.Address).HasMaxLength(500).IsRequired();
            builder.Property(i => i.Remarks).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
