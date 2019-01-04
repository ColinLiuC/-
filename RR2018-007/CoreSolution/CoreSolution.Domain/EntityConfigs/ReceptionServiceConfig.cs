using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ReceptionServiceConfig : IEntityTypeConfiguration<ReceptionService>
    {
        public void Configure(EntityTypeBuilder<ReceptionService> builder)
        {
            builder.ToTable("T_ReceptionService").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ServiceName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Category);
            builder.Property(i => i.ServiceDescription).HasMaxLength(500);
            builder.Property(i => i.ServiceFlow).HasMaxLength(200);
            builder.Property(i => i.ServiceBasis).HasMaxLength(200);
            builder.Property(i => i.ApplicationConditions).HasMaxLength(200);
            builder.Property(i => i.Attachments);          
            builder.Property(i => i.ServiceAddress).HasMaxLength(100);
            builder.Property(i => i.TimeDescription).HasMaxLength(100);
            builder.Property(i => i.CaChargeSituationtegory);
            builder.Property(i => i.ServiceCost);
            builder.Property(i => i.MattersAttention).HasMaxLength(200);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
