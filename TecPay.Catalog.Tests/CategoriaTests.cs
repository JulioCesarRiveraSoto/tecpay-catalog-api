using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Domain.Entities;
using Xunit;

namespace TecPay.Catalog.Tests
{
    public class CategoriaTests
    {
        [Fact]
        public void Create_Categoria_Con_Datos_Validos()
        {
            var categoria = new Categoria("Computación");

            Assert.True(categoria.FechaBajaUtc == null); 
            Assert.Equal("Computación", categoria.CategoriaNombre);
        }

        [Fact]
        public void Deactivate_Categoria()
        {
            var categoria = new Categoria("Computación");

            categoria.Deactivate(); 
            Assert.False(categoria.FechaBajaUtc == null);
        }
    }

}
