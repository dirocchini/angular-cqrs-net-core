using System;
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
            var hashes = PasswordUtil.CreatePasswordHash("123");

            var admin = new User()
            {
                Id = 1,
                Name = "Administrator",
                Login = "admin",
                Password = "123",
                Created = DateTime.Now,
                PasswordHash = hashes.passwordHash,
                PasswordSalt = hashes.passwordSalt
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
                .HasColumnType("varchar (50)")
                .IsRequired();

            builder
                .Property(e => e.PasswordHash)
                .IsRequired();

            builder
                .Property(e => e.PasswordSalt)
                .IsRequired();

            builder
                .HasData(admin);
        }
    }
}
