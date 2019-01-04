using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
  public class QuestionnaireAnswerConfig : IEntityTypeConfiguration<QuestionnaireAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireAnswer> builder)
        {
            builder.ToTable("T_QuestionnaireAnswer").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.WenJuanId);
            builder.Property(i => i.TitleId);
            builder.Property(i => i.Answer).HasMaxLength(500);
            builder.Property(i => i.IsDeleted);
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
