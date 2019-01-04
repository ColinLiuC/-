using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class QuestionnaireTitleConfig : IEntityTypeConfiguration<QuestionnaireTitle>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireTitle> builder)
        {
            builder.ToTable("T_QuestionnaireTitle").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.TitleType);
            builder.Property(i => i.Title).HasMaxLength(500).IsRequired();
            builder.Property(i => i.WenJuanId);
            builder.Property(i => i.TitleImgName).HasMaxLength(500);
            builder.Property(i => i.TitleImgPath).HasMaxLength(500);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
