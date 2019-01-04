using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace CoreSolution.Domain.EntityConfigs
{
    public class ExpenditureConfig: IEntityTypeConfiguration<Expenditure>
    {
        public void Configure(EntityTypeBuilder<Expenditure> builder)
        {
            builder.ToTable("T_Expenditure").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ExpenditureName).HasMaxLength(50);
            builder.Property(i => i.Purpose).HasMaxLength(1000);
            builder.Property(i => i.DutyPeople).HasMaxLength(50);
            builder.Property(i => i.UseMoney);
            builder.Property(i => i.DutyPeople).HasMaxLength(50);
            builder.Property(i => i.Accessory).HasMaxLength(3000);
            builder.Property(i => i.AccessoryUrl).HasMaxLength(3000);
            builder.Property(i => i.UseDate);
            builder.Property(i => i.Category);
            builder.Property(i => i.Remark).HasMaxLength(500);
            builder.Property(i => i.RegisterPeople).HasMaxLength(50);
            builder.Property(i => i.RegisterDate);
            builder.Property(i => i.StreetId);
            builder.Property(i => i.StationId);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
        }
}
