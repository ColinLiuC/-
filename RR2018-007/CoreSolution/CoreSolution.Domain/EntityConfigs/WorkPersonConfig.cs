﻿using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.EntityConfigs
{
    public class WorkPersonConfig : IEntityTypeConfiguration<WorkPerson>
    {
        public void Configure(EntityTypeBuilder<WorkPerson> builder)
        {
            builder.ToTable("T_WorkPersons").HasQueryFilter(i => !i.IsDeleted);
            builder.HasKey(i => i.Id);
            builder.Property(i => i.PerImg).HasMaxLength(200);
            builder.Property(i => i.PerImgSrc).HasMaxLength(1000);

            builder.Property(i => i.IsDeleted).IsRequired();
            builder.Property(i => i.CreationTime);
            builder.Property(i => i.LastModificationTime);
            builder.Property(i => i.DeletionTime);

        }
    }
}