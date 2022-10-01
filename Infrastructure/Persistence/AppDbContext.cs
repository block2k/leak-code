using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor { get; set; }

        public AppDbContext(
            DbContextOptions<AppDbContext> opt,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor
            ) : base(opt)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Student>? Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}