using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class DesireManagementConfig:IEntityTypeConfiguration<DesireManagement>
    {
        public void Configure(EntityTypeBuilder<DesireManagement> builder)
        {
            builder.ToTable("T_DesireManagement").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Publisher).HasMaxLength(50);
            builder.Property(i => i.ContactNumber);
            builder.Property(i => i.DesireCategory);
            builder.Property(i => i.StreetId);
            builder.Property(i => i.StreetName).HasMaxLength(50);
            builder.Property(i => i.JuWeiId);
            builder.Property(i => i.DetailedAddress).HasMaxLength(500);
            builder.Property(i => i.DesireName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.DesireContent).HasMaxLength(500);
            builder.Property(i => i.CurrentState);
            builder.Property(i => i.ClaimPeriod);
            builder.Property(i => i.ReportPerson).HasMaxLength(50);
            builder.Property(i => i.ReportDate);
            builder.Property(i => i.Claimant).HasMaxLength(50);
            builder.Property(i => i.ClaimantContactNumber);
            builder.Property(i => i.ClaimantAddress).HasMaxLength(500);
            builder.Property(i => i.ClaimantStreetId);
            builder.Property(i => i.ClaimantJuWeiId);
            builder.Property(i => i.ClaimSituation).HasMaxLength(200);
            builder.Property(i => i.ClaimDate);
            builder.Property(i => i.RealizationSituation).HasMaxLength(500);
            builder.Property(i => i.EvaluationOpinion).HasMaxLength(500);
            builder.Property(i => i.Registrant).HasMaxLength(50);
            builder.Property(i => i.RegistionDate);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
