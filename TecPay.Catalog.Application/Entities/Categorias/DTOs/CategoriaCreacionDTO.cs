using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Entities.Categorias.DTOs
{
    public class CategoriaCreacionDTO
    {
        [Required]
        [Display(Name = "Categoría")]
        public string CategoriaNombre { get; set; }

    }
}
