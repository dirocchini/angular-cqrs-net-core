﻿using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedOps;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin = new User()
            {
                Id = 1,
                Name = "Administrator",
                Login = "admin",
                Password = "123".Crypt(),
                Created = DateTime.Now
            };

            builder
                .ToTable("Users");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .HasColumnType("varchar (50)")
                .IsRequired();

            builder
                .Property(e => e.Email)
                .HasColumnType("varchar (50)");


            builder
                .Property(e => e.Login)
                .HasColumnType("varchar (50)")
                .IsRequired();

            builder
                .HasIndex(e => e.Login)
                .IsUnique();

            builder
                .Property(e => e.Password)
                .HasColumnType("varchar (500)")
                .IsRequired();

            builder
                .HasData(admin);
        }
    }
}
