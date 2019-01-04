using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("T_AppUser").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.UserName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RealName).HasMaxLength(50);
            builder.Property(i => i.Email).HasMaxLength(50);
            builder.Property(i => i.Phone).HasMaxLength(50);
            builder.Property(i => i.PassWord).HasMaxLength(50).IsRequired();
            builder.Property(i => i.IdCard).HasMaxLength(50);
            builder.Property(i => i.Gender).HasMaxLength(20);
            builder.Property(i => i.Age);
            builder.Property(i => i.UserQRCode).HasMaxLength(100);
            builder.Property(i => i.Picture).HasMaxLength(100);
            builder.Property(i => i.Integration);
            builder.Property(i => i.IsLocked);
            builder.Property(i => i.IsPhoneNumConfirmed);
            builder.Property(i => i.IsEmailConfirmed);
            builder.Property(i => i.Salt).HasMaxLength(50);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        }
    }
}
