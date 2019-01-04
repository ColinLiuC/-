using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSolution.Domain.EntityConfigs
{
    public class NoticeConfig : IEntityTypeConfiguration<Notice>
    {
        public void Configure(EntityTypeBuilder<Notice> builder)
        {
            builder.ToTable("T_Notices").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.NoticeTitle).HasMaxLength(50).IsRequired();
            builder.Property(i => i.NoticeInfo).HasMaxLength(500).IsRequired();
            builder.Property(i => i.NoticeChannel).HasMaxLength(50).IsRequired();
            builder.Property(i => i.NoticePeople).HasMaxLength(50).IsRequired();
            builder.Property(i => i.NoticeTime).HasMaxLength(50);
            builder.Property(i => i.NoticeState).HasMaxLength(50).IsRequired();
            builder.Property(i => i.StreetId).HasMaxLength(100);
            builder.Property(i => i.StreetName).HasMaxLength(50);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
         
        }
    }
}
