using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class ServiceApplicationConfig : IEntityTypeConfiguration<ServiceApplication>
    {
        public void Configure(EntityTypeBuilder<ServiceApplication> builder)
        {
            builder.ToTable("T_ServiceApplication").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ApplicantName).HasMaxLength(100).IsRequired();
            builder.Property(i => i.ContactNumber);
            builder.Property(i => i.Remarks).HasMaxLength(500);
            builder.Property(i => i.Registrant).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RegisterDate);
            builder.Property(i => i.ServiceId).IsRequired();
        }
    }
}
