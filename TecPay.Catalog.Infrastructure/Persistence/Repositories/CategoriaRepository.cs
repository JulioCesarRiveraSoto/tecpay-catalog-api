using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Infrastructure.Persistence.Repositories
{
    public sealed class CategoriaRepository : CustomBaseRepository, ICategoriaRepository
    {
        private readonly CatalogDbContext _context;
        public CategoriaRepository(CatalogDbContext db) : base() 
        {
            this._context = db;
        }

        public async Task<List<Categoria>> SearchAsync(string? categoriaNombre, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Categoria.AsQueryable();

            queryable = queryable.Where(x => x.FechaBajaUtc == null);
            if (!string.IsNullOrWhiteSpace(categoriaNombre))
                queryable = queryable.Where(x => x.CategoriaNombre.ToUpper().Contains(categoriaNombre.ToUpper()));

            return await queryable.ToListAsync();
        }

        public async Task<PagedResult<Categoria>> SearchPaginadoAsync(string? categoriaNombre, int numeroPagina, int tamanioPagina, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Categoria.AsQueryable();
            
            queryable = queryable.Where(x => x.FechaBajaUtc == null);
            if (!string.IsNullOrWhiteSpace(categoriaNombre))
                queryable = queryable.Where(x => x.CategoriaNombre.ToUpper().Contains(categoriaNombre.ToUpper()));

            var total = await queryable.CountAsync(cancellationToken);

            var categorias = await queryable
                .OrderBy(x => x.CategoriaNombre)
                .Skip((numeroPagina - 1) * tamanioPagina)
                .Take(tamanioPagina)
                .ToListAsync(cancellationToken);

            return new PagedResult<Categoria>(categorias, numeroPagina, tamanioPagina, total);
        }

        public Task<Categoria?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Categoria.AsQueryable();

            return this.Get<Categoria>(id, queryable, cancellationToken);
        }

        public Task<List<Categoria?>> GetAllAsync(CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Categoria.AsQueryable();

            queryable = queryable
                .OrderBy(x => x.CategoriaNombre);

            return this.Get<Categoria>(queryable, cancellationToken);
        }

        public Task<bool> ExistsCategoriaAsync(string categoriaNombre, Guid? ignoreId, CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Categoria.AsQueryable();

            return queryable.AnyAsync(x => x.CategoriaNombre == categoriaNombre.ToUpper() && (!ignoreId.HasValue || x.Id != ignoreId), cancellationToken);
        }

        public Task AddAsync(Categoria categoria, CancellationToken cancellationToken)
        {
            return _context.Set_Categoria.AddAsync(categoria, cancellationToken).AsTask();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<Categoria?>> GetComboReloadAsync(CancellationToken cancellationToken)
        {
            var queryable = _context.Set_Categoria.AsQueryable();

            return this.Get<Categoria>(queryable, cancellationToken);
        }

    }
}
