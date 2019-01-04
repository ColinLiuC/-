using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
  public class QuestionnaireManageConfig : IEntityTypeConfiguration<QuestionnaireManage>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireManage> builder)
        {
            builder.ToTable("T_QuestionnaireManage").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.QuestionnaireType);
            builder.Property(i => i.QuestionnaireName).HasMaxLength(500).IsRequired();
            builder.Property(i => i.CurrentState);       
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
