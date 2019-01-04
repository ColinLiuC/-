using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
  public  class QuestionBankManageConfig : IEntityTypeConfiguration<QuestionBankManage>
    {
        public void Configure(EntityTypeBuilder<QuestionBankManage> builder)
        {
            builder.ToTable("T_QuestionBankManage").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.TitleType);
            builder.Property(i=>i.TitleName).HasMaxLength(500).IsRequired();
            builder.Property(i => i.TitleRemarks).HasMaxLength(500);
            builder.Property(i => i.TitleOptions).HasMaxLength(500);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);          
        }
    }
}
