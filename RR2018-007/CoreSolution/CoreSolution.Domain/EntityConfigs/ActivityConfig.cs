using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ActivityConfig : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("T_Activity").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ActivityName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ActivityType);
            builder.Property(i => i.HostUnit).HasMaxLength(100);
            builder.Property(i => i.PersonCharge).HasMaxLength(50);
            builder.Property(i => i.ContactNumber).HasMaxLength(50);
            builder.Property(i => i.MeetingRoom);
            builder.Property(i => i.ActivityAddress).HasMaxLength(200);
            builder.Property(i => i.StartTime);
            builder.Property(i => i.EndTime);
            builder.Property(i => i.Street);
            builder.Property(i => i.StreetName).HasMaxLength(50);
            builder.Property(i => i.PostStation);
            builder.Property(i => i.PostStationName).HasMaxLength(50);
            builder.Property(i=>i.NumberParticipants);
            builder.Property(i => i.ExpectedNumberParticipants);
            builder.Property(i => i.DetailsActivities);
            builder.Property(i => i.Attachments);
            builder.Property(i => i.QRCode);
            builder.Property(i => i.AduitIsPass);
            builder.Property(i => i.AduitRemarks).HasMaxLength(500);
            builder.Property(i => i.Auditor);
            builder.Property(i => i.Flag);
            builder.Property(i => i.TopDate);
            builder.Property(i => i.IsGuiDang);
            builder.Property(i => i.ArchivalRemark).HasMaxLength(500);
            builder.Property(i => i.Archiving).HasMaxLength(50);
            builder.Property(i => i.FilingDate);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i=>i.ActivityImg);
        }
    }
}
