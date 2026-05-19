using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Domain.Common;

namespace TecPay.Catalog.Domain.Entities
{
    [Table(nameof(Producto))]
    public class Producto : Entidad
    {
        public Producto(string productoNombre, string productoSKU, Guid fK_IDCategoria, decimal productoPrecio, int productoStock, string? productoDescripcion)
        {
            SetCoreData(productoNombre, productoSKU, fK_IDCategoria, productoPrecio, productoStock, productoDescripcion);
        }


        [Required]
        [Display(Name = "Producto")]
        public string ProductoNombre { get; private set; } = string.Empty;


        [Required]
        [Display(Name = "SKU")]
        public string ProductoSKU { get; private set; } = string.Empty;


        [Required]
        [Display(Name = "Categoría")]
        public Guid FK_IDCategoria { get; set; }
        [ForeignKey(nameof(FK_IDCategoria))]
        [Display(AutoGenerateField = false)]
        public virtual Categoria Categoria { get; set; }


        [Required]
        [Display(Name = "Precio")]
        public decimal ProductoPrecio { get; private set; }


        [Required]
        [Display(Name = "Stock")]
        public int ProductoStock { get; private set; }


        [Display(Name = "Descripción")]
        public string? ProductoDescripcion { get; private set; }


        public void Update(string productoNombre, string productoSKU, Guid fK_IDCategoria, decimal productoPrecio, int productoStock, string? productoDescripcion)
        {
            SetCoreData(productoNombre, productoSKU, fK_IDCategoria, productoPrecio, productoStock, productoDescripcion);
        }

        public void Deactivate()
        {
            Touch();
        }

        private void SetCoreData(string productoNombre, string productoSKU, Guid fK_IDCategoria, decimal productoPrecio, int productoStock, string? productoDescripcion)
        {
            if (string.IsNullOrWhiteSpace(productoNombre)) 
                throw new ArgumentException("Nombre de Producto es Requerido.");
            if (string.IsNullOrWhiteSpace(productoSKU)) 
                throw new ArgumentException("SKU es Requerido.");
            if (fK_IDCategoria == Guid.Empty)
                throw new ArgumentException("La Categoría es requerida.", nameof(fK_IDCategoria));
            if (productoPrecio <= 0) 
                throw new ArgumentException("Precio debe ser mayor que 0.");
            if (productoStock < 0) 
                throw new ArgumentException("Stock no puede ser negativo.");

            ProductoNombre = productoNombre.Trim();
            ProductoSKU = productoSKU.Trim().ToUpperInvariant();
            FK_IDCategoria = fK_IDCategoria;
            ProductoPrecio = productoPrecio;
            ProductoStock = productoStock;
            ProductoDescripcion = productoDescripcion?.Trim();
            this.FechaActualizacionUtc = DateTime.Now;
        }
    }
}
