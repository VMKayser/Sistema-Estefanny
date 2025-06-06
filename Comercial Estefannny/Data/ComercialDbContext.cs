using Microsoft.EntityFrameworkCore;
using Comercial_Estefannny.ViewModel;
using System.IO;

namespace Comercial_Estefannny.Data
{
    /// <summary>
    /// Contexto de base de datos moderno para .NET 8.0 usando Entity Framework Core
    /// </summary>
    public class ComercialDbContext : DbContext
    {
        public DbSet<ClientesC> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuración moderna para SQLite en .NET 8.0
            var connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "comercial_Estefanny_base_datos.db")}";
            
            optionsBuilder.UseSqlite(connectionString)
                         .EnableSensitiveDataLogging(false)
                         .EnableServiceProviderCaching()
                         .EnableDetailedErrors()
                         .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de entidades con Fluent API
            ConfigureClientesEntity(modelBuilder);
            ConfigureProductosEntity(modelBuilder);
            ConfigureProveedoresEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureClientesEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientesC>(entity =>
            {
                entity.ToTable("cliente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(200);
                entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(e => e.Deuda).HasColumnName("deuda").HasColumnType("REAL");
            });
        }

        private static void ConfigureProductosEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("producto");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NombreProducto).HasColumnName("nombreProducto").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
                entity.Property(e => e.PrecioCompra).HasColumnName("precioCompra").HasColumnType("REAL");
                entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta").HasColumnType("REAL");
                entity.Property(e => e.CodigoBarras).HasColumnName("codigoBarras").HasMaxLength(50);
                entity.Property(e => e.Variante).HasColumnName("variante").HasMaxLength(50);
                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
                entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            });
        }

        private static void ConfigureProveedoresEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proveedores>(entity =>
            {
                entity.ToTable("proveedor");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(200);
                entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(e => e.Deuda).HasColumnName("deuda").HasColumnType("REAL");
            });
        }
    }
}
