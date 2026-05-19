using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Infrastructure.Persistence.Repositories
{
    public sealed class ProductoRepository : CustomBaseRepository, IProductoRepository
    {
        private readonly CatalogDbContext _context;
        public ProductoRepository(CatalogDbContext db) : base() 
        {
            this._context = db;
        }

        public async Task<List<Producto>> SearchAsync(string? buscar, string? categoriaNombre, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();

            queryable = queryable
                .Include(x => x.Categoria).AsNoTracking();

            queryable = queryable.Where(x => x.FechaBajaUtc == null);
            if (!string.IsNullOrWhiteSpace(buscar))
                queryable = queryable.Where(x => x.ProductoNombre.ToUpper().Contains(buscar.ToUpper()) || x.ProductoSKU.ToUpper().Contains(buscar.ToUpper()));

            if (!string.IsNullOrWhiteSpace(categoriaNombre))
                queryable = queryable.Where(x => x.Categoria.CategoriaNombre.ToUpper().Contains(categoriaNombre.ToUpper()));

            return await queryable.ToListAsync();
        }

        public async Task<PagedResult<Producto>> SearchPaginadoAsync(string? buscar, string? categoriaNombre, int numeroPagina, int tamanioPagina, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();
            
            queryable = queryable
                .Include(x => x.Categoria).AsNoTracking();

            queryable = queryable.Where(x => x.FechaBajaUtc == null);
            if (!string.IsNullOrWhiteSpace(buscar))
                queryable = queryable.Where(x => x.ProductoNombre.ToUpper().Contains(buscar.ToUpper()) 
                || x.ProductoSKU.ToUpper().Contains(buscar.ToUpper())
                || x.Categoria.CategoriaNombre.ToUpper().Contains(categoriaNombre.ToUpper()));

            //if (!string.IsNullOrWhiteSpace(categoriaNombre))
            //    queryable = queryable.Where(x => x.Categoria.CategoriaNombre.ToUpper().Contains(categoriaNombre.ToUpper()));

            var total = await queryable.CountAsync(cancellationToken);

            var productos = await queryable
                .OrderBy(x => x.ProductoNombre)
                .Skip((numeroPagina - 1) * tamanioPagina)
                .Take(tamanioPagina)
                .ToListAsync(cancellationToken);

            return new PagedResult<Producto>(productos, numeroPagina, tamanioPagina, total);
        }

        public Task<Producto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();

            queryable = queryable
                .Include(x => x.Categoria);

            return this.Get<Producto>(id, queryable, cancellationToken);
        }

        public Task<List<Producto?>> GetAllAsync(CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();

            queryable = queryable
                .OrderBy(x => x.ProductoNombre);

            return this.Get<Producto>(queryable, cancellationToken);
        }

        public Task<bool> ExistsProductoAsync(string productoNombre, Guid? ignoreId, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();

            return queryable.AnyAsync(x => x.ProductoSKU == productoNombre.ToUpper() && (!ignoreId.HasValue || x.Id != ignoreId), cancellationToken);
        }

        public Task<bool> SkuExistsAsync(string sku, Guid? ignoreId, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();

            return queryable.AnyAsync(x => x.ProductoSKU == sku.ToUpper() && (!ignoreId.HasValue || x.Id != ignoreId), cancellationToken);
        }

        public Task AddAsync(Producto producto, CancellationToken cancellationToken)
        {
            return _context.Set_Producto.AddAsync(producto, cancellationToken).AsTask();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<Producto?>> GetComboReloadAsync(CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Producto.AsQueryable();

            return this.Get<Producto>(queryable, cancellationToken);
        }

    }
}
