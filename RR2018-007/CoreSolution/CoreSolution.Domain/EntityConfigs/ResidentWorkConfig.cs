using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ResidentWorkConfig : IEntityTypeConfiguration<ResidentWork>
    {
        public void Configure(EntityTypeBuilder<ResidentWork> builder)
        {
            builder.ToTable("T_ResidentWork").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ResidentWorkName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ResidentWorkType).IsRequired();
            builder.Property(i => i.ResidentWorkFlow);
            builder.Property(i => i.ResidentWorkFlowImg).HasMaxLength(500);
            builder.Property(i => i.ResidentWorkFlowImgPaths).HasMaxLength(500);
            builder.Property(i => i.RelevantPolicies);
            builder.Property(i => i.AdministrativeBasis);
            builder.Property(i => i.Requirement);
            builder.Property(i => i.Material);
            builder.Property(i => i.Charge);
            builder.Property(i => i.Deadline).HasMaxLength(50);
            builder.Property(i => i.IsPublish).IsRequired();
            builder.Property(i => i.IsGuiDang);
            builder.Property(i => i.GuiDangRenark);
            builder.Property(i => i.GuiDangTime);
            builder.Property(i => i.GuiDangUser).HasMaxLength(50);

            builder.Property(i => i.StreetId);
            builder.Property(i => i.StationIds).HasMaxLength(500);
            builder.Property(i => i.StationNames).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }

    }
}
