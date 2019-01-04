using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class KarmaMemberConfig : IEntityTypeConfiguration<KarmaMember>
    {
        public void Configure(EntityTypeBuilder<KarmaMember> builder)
        {
            builder.ToTable("T_KarmaMember").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.KarmaId).IsRequired();
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.IDCard).HasMaxLength(50).IsRequired();

            builder.Property(i => i.ContactTel).HasMaxLength(50);
            builder.Property(i => i.Address).HasMaxLength(500);
            builder.Property(i => i.Duties).IsRequired();

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}