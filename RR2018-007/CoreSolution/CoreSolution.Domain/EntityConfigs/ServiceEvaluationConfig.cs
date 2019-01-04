using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class ServiceEvaluationConfig : IEntityTypeConfiguration<ServiceEvaluation>
    {
        public void Configure(EntityTypeBuilder<ServiceEvaluation> builder)
        {
            builder.ToTable("T_ServiceEvaluation").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);                  
            builder.Property(i => i.UserGuid);
            builder.Property(i => i.Evaluator);
            builder.Property(i => i.ServiceGuid);
            builder.Property(i => i.EvaluationDate);
            builder.Property(i => i.EvaluationContent);
            builder.Property(i => i.EvaluatorImgPath);
            builder.Property(i => i.Satisfaction);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
