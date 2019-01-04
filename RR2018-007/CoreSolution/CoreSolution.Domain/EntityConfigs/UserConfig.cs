using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSolution.Domain.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.UserName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.RealName).HasMaxLength(50);
            builder.Property(i => i.Email).HasMaxLength(50);
            builder.Property(i => i.IsEmailConfirmed);
            builder.Property(i => i.PhoneNum).HasMaxLength(20);
            builder.Property(i => i.IsPhoneNumConfirmed);
            builder.Property(i => i.Password).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Salt).HasMaxLength(50).IsRequired();
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.IsLocked).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
        

            builder.Property(i => i.IdCard);
            builder.Property(i => i.PeopleSex).HasMaxLength(20);
            builder.Property(i => i.PeopleAge).HasMaxLength(20); ;
            builder.Property(i => i.PeopleTell).HasMaxLength(50);
            builder.Property(i => i.PeopleCard).HasMaxLength(100);
            builder.Property(i => i.PeoplePicture).HasMaxLength(100);
            builder.Property(i => i.PeopleIntegration).HasMaxLength(50);
        }
    }
}
