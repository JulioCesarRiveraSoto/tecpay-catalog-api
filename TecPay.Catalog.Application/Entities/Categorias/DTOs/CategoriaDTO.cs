using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecPay.Catalog.Application.Entities.Categorias.DTOs
{
    public class CategoriaDTO
    {
        public Guid Id { get; set; }


        [Required]
        [Display(Name = "Categoría")]
        public string CategoriaNombre { get; set; }

    }

    public class CategoriaComboDTO
    {
        public string Id { get; set; }

        public string CategoriaNombre { get; set; }
    }

    public class CategoriaListaDTO
    {

        public string Id { get; set; }
        public string CategoriaNombre { get; set; }
    }
}
