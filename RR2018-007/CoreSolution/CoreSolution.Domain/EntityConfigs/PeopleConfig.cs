using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PeopleConfig : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.ToTable("T_Peoples").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.PeopleNum).HasMaxLength(50).IsRequired();
            builder.Property(i => i.PassWord).HasMaxLength(50).IsRequired();
            builder.Property(i => i.PeopleName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.PeopleTell).HasMaxLength(50).IsRequired();
            builder.Property(i => i.PeopleMail).HasMaxLength(100);
            builder.Property(i => i.PeopleCard).HasMaxLength(100);
            builder.Property(i => i.PeoplePicture).HasMaxLength(100);
            builder.Property(i => i.PeopleAge).HasMaxLength(20);
            builder.Property(i => i.PeopleSex).HasMaxLength(20);
            builder.Property(i => i.PeopleIntegration).HasMaxLength(50);
            builder.Property(i => i.IdCard);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);

        }

    }
}
