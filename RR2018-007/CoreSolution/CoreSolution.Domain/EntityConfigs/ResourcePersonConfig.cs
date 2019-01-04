using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ResourcePersonConfig : IEntityTypeConfiguration<ResourcePerson>
    {
        public void Configure(EntityTypeBuilder<ResourcePerson> builder)
        {
            builder.ToTable("T_ResourcePerson").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);

        }
    }
}
