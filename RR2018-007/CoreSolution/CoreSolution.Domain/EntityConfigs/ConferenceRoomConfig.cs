using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ConferenceRoomConfig : IEntityTypeConfiguration<ConferenceRoom>
    {
        public void Configure(EntityTypeBuilder<ConferenceRoom> builder)
        {
            builder.ToTable("T_ConferenceRoom").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ConferenceRoomName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ConferenceRoomType);
            builder.Property(i => i.CompetentUnit).HasMaxLength(500);
            builder.Property(i => i.StreetId);
            builder.Property(i => i.PostStation);
            builder.Property(i => i.PersonInCharge).HasMaxLength(50);
            builder.Property(i => i.ContactPhone).HasMaxLength(20);
            builder.Property(i => i.Pedestal);
            builder.Property(i => i.Address).HasMaxLength(500);
            builder.Property(i => i.DetailedDescr).HasMaxLength(500);
            builder.Property(i => i.ImgName).HasMaxLength(50);       
            builder.Property(i => i.ImgPath);
            builder.Property(i => i.State);
            builder.Property(i => i.IsCharge);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);           
        }
    }
}
