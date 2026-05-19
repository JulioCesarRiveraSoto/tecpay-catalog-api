using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Infrastructure
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Set_Producto => Set<Producto>();
        public DbSet<Categoria> Set_Categoria => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Producto>();
            e.ToTable(nameof(Producto)); e.HasKey(x => x.Id);
            e.Property(x => x.ProductoNombre).HasMaxLength(200).IsRequired();
            e.Property(x => x.ProductoSKU).HasMaxLength(40).IsRequired();
            e.HasIndex(x => x.ProductoSKU).IsUnique();
            e.Property(x => x.FK_IDCategoria).IsRequired();
            e.Property(x => x.ProductoPrecio).HasPrecision(18, 2);
            e.Property(x => x.ProductoDescripcion).HasMaxLength(500);
        }
    }
}
