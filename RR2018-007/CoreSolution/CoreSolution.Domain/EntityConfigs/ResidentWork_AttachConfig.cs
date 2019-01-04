using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ResidentWork_AttachConfig : IEntityTypeConfiguration<ResidentWork_Attach>
    {
        public void Configure(EntityTypeBuilder<ResidentWork_Attach> builder)
        {
            builder.ToTable("T_ResidentWork_Attach").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ResidentWorkId).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();
            builder.Property(i => i.StationId).IsRequired();

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }

    }
}
