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
                .HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
