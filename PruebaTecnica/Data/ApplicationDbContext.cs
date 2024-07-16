using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<TasaCambio> TasaCambios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id); // Establecer la clave primaria
                entity.Property(c => c.Nombre).IsRequired(); // Nombre es requerido
                entity.Property(c => c.Email).HasMaxLength(100); // Limitar el tamaño de Email
                entity.Property(c => c.Telefono).HasMaxLength(20); // Limitar el tamaño de Teléfono
                                                                  
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(p => p.Id); // Establecer la clave primaria
                entity.Property(p => p.Nombre).IsRequired(); // Nombre es requerido
                entity.Property(p => p.CodigoProducto).HasMaxLength(20); // Limitar el tamaño de CódigoProducto
                entity.Property(p => p.CodigoBarra).HasMaxLength(20); // Limitar el tamaño de CódigoBarra
                entity.Property(p => p.SKU).HasMaxLength(20); // Limitar el tamaño de SKU
                                                            
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(o => o.Id); // Establecer la clave primaria
                entity.Property(o => o.OrdenFecha).IsRequired(); // OrdenFecha es requerido
                entity.Property(o => o.CantidadTotal).HasColumnType("decimal(18,2)"); // Definir tipo y precisión para CantidadTotal
                entity.Property(o => o.Impuestos).HasColumnType("decimal(18,2)"); // Definir tipo y precisión para Impuestos

                // Relación con Cliente (uno a muchos)
                entity.HasOne(o => o.Cliente)
                    .WithMany(c => c.Ordenes)
                    .HasForeignKey(o => o.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade); // Agregar comportamiento de eliminación en cascada
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(f => f.Id); // Establecer la clave primaria
                entity.Property(f => f.FacturaFecha).IsRequired(); // FacturaFecha es requerido
                entity.Property(f => f.Cantidad).HasColumnType("decimal(18,2)"); // Definir tipo y precisión para Cantidad

                // Relación con Orden (uno a muchos)
                entity.HasOne(f => f.Orden)
                    .WithMany(o => o.Facturas)
                    .HasForeignKey(f => f.OrdenId)
                    .OnDelete(DeleteBehavior.Cascade); // Agregar comportamiento de eliminación en cascada
            });

            modelBuilder.Entity<TasaCambio>(entity =>
            {
                entity.HasKey(tc => tc.Id); // Establecer la clave primaria
                entity.Property(tc => tc.Moneda).IsRequired().HasMaxLength(3); // Moneda es requerida y tiene un tamaño máximo de 3 caracteres
                entity.Property(tc => tc.Tasa).HasColumnType("decimal(18,6)"); // Definir tipo y precisión para Tasa
                entity.Property(tc => tc.Fecha).IsRequired(); // Fecha es requerido
                                                              
            });
        }


    }
}
