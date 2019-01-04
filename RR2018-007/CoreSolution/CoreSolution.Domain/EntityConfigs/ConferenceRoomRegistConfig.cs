using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
   public class ConferenceRoomRegistConfig : IEntityTypeConfiguration<ConferenceRoomRegist>
    {
        public void Configure(EntityTypeBuilder<ConferenceRoomRegist> builder)
        {
            builder.ToTable("T_ConferenceRoomRegist").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ConferenceTheme).HasMaxLength(200).IsRequired();
            builder.Property(i => i.ConferenceType);
            builder.Property(i => i.HostUnit).HasMaxLength(100);
            builder.Property(i => i.PersonInCharge).HasMaxLength(50);
            builder.Property(i => i.ContactNumber).HasMaxLength(50);
            builder.Property(i => i.ApplicationUnit).HasMaxLength(100);
            builder.Property(i => i.Applicant).HasMaxLength(50);
            builder.Property(i => i.ApplicanContactNumbert).HasMaxLength(50);
            builder.Property(i => i.StartDate);
            builder.Property(i => i.EndDate);
            builder.Property(i => i.participants);
            builder.Property(i => i.IsOpen);
            builder.Property(i => i.ServiceClassification);
            builder.Property(i => i.PreConferenceService);
            builder.Property(i => i.ServiceInMeeting);
            builder.Property(i => i.PostConferenceService);
            builder.Property(i => i.Remarks);
            builder.Property(i => i.TimeStamp);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
        }
    }
}
