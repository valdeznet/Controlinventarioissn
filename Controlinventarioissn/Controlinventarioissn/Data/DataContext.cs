using Controlinventarioissn.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Controlinventarioissn.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Deposito> Depositos { get; set; } //que quiero mapear, El deposito de equipamiento
        public DbSet<Category> Categories { get; set; } //que quiero mapear, la categoria de los Equipamientos
        public DbSet<Delegacion> Delegaciones { get; set; } //que quiero mapear, la delegacion
        public object Cotegories { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Delegacion>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Deposito>().HasIndex(c => c.Name).IsUnique();
        }
    }

}
