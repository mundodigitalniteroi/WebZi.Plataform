using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebZi.Plataform.Domain.Models.VLock;

namespace WebZi.Plataform.Data.Database
{
    public class VLockDbContext : DbContext
    {
        public VLockDbContext(DbContextOptions<VLockDbContext> options) : base(options)
        {
        }

        public DbSet<DispositivosModel> Dispositivos { get; set; }
        public DbSet<RecolhimentoModel> Recolhimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
