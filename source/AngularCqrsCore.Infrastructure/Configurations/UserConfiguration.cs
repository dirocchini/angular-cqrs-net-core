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
                .HasColumnType("varchar (50)");


            builder
                .HasData(
                    new User()
                    {
                        Id = 1,
                        Name = "Diego Roquini",
                        Login = "droquini"
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "iago Stanchese",
                        Login = "istanchese"
                    },
                    new User()
                    {
                        Id = 3,
                        Name = "Fernando Vila",
                        Login = "fvila"
                    });

        }
    }
}
