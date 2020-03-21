using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence.Configurations;

namespace Persistence
{
    public sealed class AngularCoreContext : DbContext, IApplicationDbContext
    {
        public AngularCoreContext(DbContextOptions<AngularCoreContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
                Database.Migrate();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = 1; //TODO FIX IT
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = 1; //TODO FIX IT
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
