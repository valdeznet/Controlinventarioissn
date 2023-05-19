using Controlinventarioissn.Data.Entities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Sector> Sectors { get; set; } //que quiero mapear, El Sector de Delegacion 
        public object Cotegories { get; internal set; }

        public DbSet<Equipamiento> Equipamientos { get; set; }

        public DbSet<EquipamientoCategory> EquipamientoCategories { get; set; }

        public DbSet<EquipamientoImage> EquipamientoImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Delegacion>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Deposito>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Sector>().HasIndex("Name", "DelegacionId").IsUnique();
            modelBuilder.Entity<Equipamiento>().HasIndex(c => c.Name).IsUnique(); //no tengamos dos productos con el mismo nombre
            modelBuilder.Entity<EquipamientoCategory>().HasIndex("EquipamientoId", "CategoryId").IsUnique();//el mismo producto en la misma categoria

        }
    }

}
