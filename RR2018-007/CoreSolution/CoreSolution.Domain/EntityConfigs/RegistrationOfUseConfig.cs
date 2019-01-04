using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class RegistrationOfUseConfig : IEntityTypeConfiguration<RegistrationOfUse>
    {
        public void Configure(EntityTypeBuilder<RegistrationOfUse> builder)
        {
            builder.ToTable("T_RegistrationOfUse").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FixedAssetsId);
            builder.Property(i => i.ReceivePeople).HasMaxLength(50);
            builder.Property(i => i.ReceiveDate);
            builder.Property(i => i.PredictReturnDate);
            builder.Property(i => i.CurrentState);
            builder.Property(i => i.ReturnDate);
            builder.Property(i => i.RegisterPeople).HasMaxLength(50);
            builder.Property(i => i.RegisterDate);
            builder.Property(i => i.Remark);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
