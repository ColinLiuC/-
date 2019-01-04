using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PublicIncomeConfig : IEntityTypeConfiguration<PublicIncome>
    {
        public void Configure(EntityTypeBuilder<PublicIncome> builder)
        {
            builder.ToTable("T_PublicIncome").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.BeYearMonth).IsRequired();
            builder.Property(i => i.QuartersId).IsRequired();
            builder.Property(i => i.JuWeiId).IsRequired();
            builder.Property(i => i.StreetId).IsRequired();

            builder.Property(i => i.LastWeekBalance).IsRequired();
            builder.Property(i => i.NowWeekBalance).IsRequired();
            builder.Property(i => i.IncomeAmount).IsRequired();
            builder.Property(i => i.ExpenditureAmount).IsRequired();
            builder.Property(i => i.IsRepairAmount).IsRequired();

            builder.Property(i => i.RegisterUser).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RegisterTime).IsRequired();

            builder.Property(i => i.Remarks).HasMaxLength(500);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
