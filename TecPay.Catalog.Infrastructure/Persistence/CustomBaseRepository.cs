using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Domain.Interfaces;

namespace TecPay.Catalog.Infrastructure.Persistence
{
    public class CustomBaseRepository
    {
        public CustomBaseRepository() 
        {
            
        }

        protected async Task<TEntidad?> Get<TEntidad>(Guid id, IQueryable<TEntidad> queryable, CancellationToken cancellationToken)
            where TEntidad : class, IEntidad
        {
            try
            {
                queryable = queryable.Where(x => x.Id == id && x.FechaBajaUtc == null);
                var entidad = await queryable.FirstOrDefaultAsync(cancellationToken);
                return entidad;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        protected async Task<List<TEntidad?>> Get<TEntidad>(IQueryable<TEntidad> queryable, CancellationToken cancellationToken)
            where TEntidad : class, IEntidad
        {
            try
            {
                queryable = queryable.Where(x => x.FechaBajaUtc == null);
                var entidades = await queryable.AsNoTracking().ToListAsync(cancellationToken);
                return entidades;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }
    }
}
