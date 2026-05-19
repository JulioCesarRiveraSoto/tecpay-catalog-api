using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Categorias.Interfaces;
using TecPay.Catalog.Application.Entities.Productos.Interfaces;
using TecPay.Catalog.Infrastructure.Persistence.Repositories;

namespace TecPay.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"];

            //services.AddDbContext<CatalogDbContext>(
            //    opt => opt.UseSqlite(configuration.GetConnectionString("CatalogDb")));

            //services.AddDbContext<CatalogDbContext>(
            //    opt => opt.UseSqlServer(configuration.GetConnectionString("DB_Context_RISO")));

            services.AddDbContext<CatalogDbContext>(options =>
            {
                if (provider == "Sqlite")
                {
                    options.UseSqlite(configuration.GetConnectionString("Sqlite"));
                }
                else if (provider == "InMemory")
                {
                    options.UseInMemoryDatabase("TecPayCatalogDb");
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("DB_Context_RISO"));
                }
            });

            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            return services;
        }

    }

}
