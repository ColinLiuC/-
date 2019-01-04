using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class ActivityEvaluationConfig : IEntityTypeConfiguration<ActivityEvaluation>
    {
        public void Configure(EntityTypeBuilder<ActivityEvaluation> builder)
        {
            builder.ToTable("T_ActivityEvaluation").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.EvaluationName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.EvaluatorImgPath);
            builder.Property(i => i.ActivityId);
            builder.Property(i => i.EvaluationContent);
            builder.Property(i => i.Score);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
