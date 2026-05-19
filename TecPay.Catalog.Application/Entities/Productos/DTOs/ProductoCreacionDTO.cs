using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Productos.DTOs
{
    public class ProductoCreacionDTO
    {
        [Required]
        [Display(Name = "Producto")]
        public string ProductoNombre { get; set; } 


        [Required]
        [Display(Name = "SKU")]
        public string ProductoSKU { get; set; } 


        [Required]
        [Display(Name = "Categoría")]
        public string FK_IDCategoria { get; set; }
        public virtual CategoriaCreacionDTO? Categoria { get; set; }


        [Required]
        [Display(Name = "Precio")]
        public decimal ProductoPrecio { get; set; }


        [Required]
        [Display(Name = "Stock")]
        public int ProductoStock { get; set; }


        [Display(Name = "Descripción")]
        public string? ProductoDescripcion { get; set; }
    }
}
