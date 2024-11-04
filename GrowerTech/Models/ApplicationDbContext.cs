using Microsoft.EntityFrameworkCore;
using GrowerTech_MVC.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrowerTech_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Agricultor> Agricultores { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<DadoClimatico> DadosClimaticos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("RM552213");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Agricultor>(entity =>
            {
                entity.ToTable("Agricultores");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Escala).IsRequired();
                entity.Property(e => e.Endereco).IsRequired();
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("Sensores");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo).IsRequired();
                entity.Property(e => e.Localizacao).IsRequired();
            });

            modelBuilder.Entity<DadoClimatico>(entity =>
            {
                entity.ToTable("DadosClimaticos");
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.Sensor)
                    .WithMany(s => s.DadosClimaticos)
                    .HasForeignKey("SensorId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && 
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;
                entity.UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
