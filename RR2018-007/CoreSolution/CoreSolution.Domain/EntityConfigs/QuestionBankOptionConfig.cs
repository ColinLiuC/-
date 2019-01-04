using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class QuestionBankOptionConfig: IEntityTypeConfiguration<QuestionBankOption>
    {
        public void Configure(EntityTypeBuilder<QuestionBankOption> builder)
        {
            builder.ToTable("T_QuestionBankOption").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.QuestionId);
            builder.Property(i => i.TitleOptions).HasMaxLength(500);
            builder.Property(i => i.IsDeleted);
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
