using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Domain.Entities;
using Xunit;

namespace TecPay.Catalog.Tests
{
    public class ProductoTests
    {
        [Fact]
        public void Create_Producto_Con_Datos_Validos()
        {
            var categoria = new Categoria("Computación");

            var producto = new Producto("Mouse", "SKU-1", categoria.Id, 250, 10, null);
            Assert.True(producto.FechaBajaUtc == null); 
            Assert.Equal("SKU-1", producto.ProductoSKU);
        }

        [Fact]
        public void Precio_Debe_Ser_Mayor_Que_0()
        {
            var categoria = new Categoria("Computación");

            Assert.Throws<ArgumentException>(() => new Producto("Mouse", "SKU-1", categoria.Id, 0, 10, null));
        }

        [Fact]
        public void Deactivate_Producto()
        {
            var categoria = new Categoria("Computación");

            var producto = new Producto("Mouse", "SKU-1", categoria.Id, 250, 10, null);
            producto.Deactivate(); 
            Assert.False(producto.FechaBajaUtc == null);
        }
    }

}
