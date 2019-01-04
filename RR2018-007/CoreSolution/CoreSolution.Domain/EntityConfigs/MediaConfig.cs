using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{

    public class MediaConfig : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("T_Medias").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.FileName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ExtensionName);
            builder.Property(i => i.FilePath);
            builder.Property(i => i.ThumbnailPath);
            builder.Property(i => i.MediaType);
            builder.Property(i => i.Width);
            builder.Property(i => i.Height);
            builder.Property(i => i.IsPublic); 
            builder.Property(i => i.IsDeleted).IsRequired(); 
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }

        

    }
}
