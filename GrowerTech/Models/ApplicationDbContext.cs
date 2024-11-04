using Microsoft.EntityFrameworkCore;
using GrowerTech_MVC.Models;

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

            // 配置 User 实体
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // 配置 Agricultor 实体
            modelBuilder.Entity<Agricultor>(entity =>
            {
                entity.ToTable("Agricultores");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Escala).IsRequired();
                entity.Property(e => e.Endereco).IsRequired();
            });

            // 配置 Sensor 实体
            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.ToTable("Sensores");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Tipo).IsRequired();
                entity.Property(e => e.Localizacao).IsRequired();
            });

            // 配置 DadoClimatico 实体
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
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added && entityEntry.Entity is BaseEntity entity)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                
                if (entityEntry.Entity is BaseEntity entityBase)
                {
                    entityBase.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added && entityEntry.Entity is BaseEntity entity)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                
                if (entityEntry.Entity is BaseEntity entityBase)
                {
                    entityBase.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}