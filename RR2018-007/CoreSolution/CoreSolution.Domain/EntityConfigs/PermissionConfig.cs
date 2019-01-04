﻿using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSolution.Domain.EntityConfigs
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("T_Permissions").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.DisplayName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(50);
            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);
            builder.HasOne(i => i.CreatorUser).WithMany().HasForeignKey(i => i.CreatorUserId);
            builder.HasOne(i => i.DeleterUser).WithMany().HasForeignKey(i => i.DeleterUserId);
        
        }
    }
}