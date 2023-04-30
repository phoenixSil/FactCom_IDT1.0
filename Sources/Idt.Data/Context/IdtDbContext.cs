using Idt.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Data.Context
{
    public class IdtDbContext: DbContext
    {
        public IdtDbContext(DbContextOptions<IdtDbContext> options): base(options)
        {

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntite>())
            {
                entry.Entity.DateDeModification = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateDeCreation = DateTime.Now;
                }

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdtDbContext).Assembly);
        }

        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
