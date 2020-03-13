using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .IsRequired(true);

            builder
                .Property(e => e.Email)
                .HasColumnType("varchar (50)");

            builder
                .Property(e => e.Login)
                .HasColumnType("varchar (50)")
                .IsRequired(true);

            builder
                .Property(e => e.Password)
                .HasColumnType("varchar (50)")
                .IsRequired(true);


            builder
                .HasData(
                    new User()
                    {
                        Id = 1,
                        Name = "Administrator",
                        Login = "admin",
                        Password = "000",
                        Created = DateTime.Now
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Diego",
                        Login = "diego-user",
                        Password = "111",
                        Created = DateTime.Now
                    },
                    new User()
                    {
                        Id = 3,
                        Name = "iago",
                        Login = "iago-user",
                        Password = "222",
                        Created = DateTime.Now
                    },
                    new User()
                    {
                        Id = 4,
                        Name = "Fernando",
                        Login = "fernando-user",
                        Password = "333",
                        Created = DateTime.Now
                    });

        }
    }
}
