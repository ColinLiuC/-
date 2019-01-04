using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class QuestionnaireOptionsConfig : IEntityTypeConfiguration<QuestionnaireOptions>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireOptions> builder)
        {
            builder.ToTable("T_QuestionnaireOptions").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.QuestionnaireTitleId);
            builder.Property(i => i.WenJuanTitleOptions).HasMaxLength(500);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
