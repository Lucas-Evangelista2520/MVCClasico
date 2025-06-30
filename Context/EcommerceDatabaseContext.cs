using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCClasico.Models;


namespace MVCClasico.Context
{
    public class EcommerceDatabaseContext : DbContext
    {
        public EcommerceDatabaseContext(DbContextOptions<EcommerceDatabaseContext> options): base(options) 
        {

        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CompraFinalizada> ComprasFinalizadas { get; set; }

        public DbSet<DetalleCompra> DetallesCompra { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configuramos para que cada compra pueda tener varios detalles de compra
            modelBuilder.Entity<CompraFinalizada>()
                .HasMany(c => c.DetallesCompra)
                .WithOne(d => d.CompraFinalizada)
                .HasForeignKey(d => d.CompraFinalizadaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
