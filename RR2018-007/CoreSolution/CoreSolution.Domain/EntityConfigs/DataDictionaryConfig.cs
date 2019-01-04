using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public  class DataDictionaryConfig: IEntityTypeConfiguration<DataDictionary>
    {
        public void Configure(EntityTypeBuilder<DataDictionary> builder)
        {
            builder.ToTable("T_DataDictionaries").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Description);
            builder.Property(i => i.Value);
            builder.Property(i => i.Sort);
            builder.Property(i => i.Tips);
            builder.Property(i => i.CustomType);
            builder.Property(i => i.ParentId);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }

        

    }
}
