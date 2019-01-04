using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PeopleAndReceptionServiceConfig : IEntityTypeConfiguration<PeopleAndReceptionService>
    {
        public void Configure(EntityTypeBuilder<PeopleAndReceptionService> builder)
        {
            builder.ToTable("T_PeopleAndReceptionServices").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.PeopleID).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ReceptinServiceID).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Type).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Comment).HasMaxLength(500);
            builder.Property(i => i.Satisfaction).HasMaxLength(50);

            builder.Property(i => i.IsDeleted).IsRequired();
        }
    }
}
