using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ProjectTrackConfig : IEntityTypeConfiguration<ProjectTrack>
    {
        public void Configure(EntityTypeBuilder<ProjectTrack> builder)
        {
            builder.ToTable("T_ProjectTrack").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.QuarterProjectId).IsRequired();
            builder.Property(i => i.TrackTime).IsRequired();
            builder.Property(i => i.ProgressDetail).HasMaxLength(500).IsRequired();
            builder.Property(i => i.ProjectTrackDetail).HasMaxLength(500);
            builder.Property(i => i.RegisterUser).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RegisterTime).IsRequired();

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
