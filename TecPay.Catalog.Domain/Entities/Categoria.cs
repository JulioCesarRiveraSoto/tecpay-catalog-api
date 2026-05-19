using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TecPay.Catalog.Domain.Common;
using TecPay.Catalog.Domain.Interfaces;

namespace TecPay.Catalog.Domain.Entities
{
    [Table(nameof(Categoria))]
    public class Categoria : Entidad
    {
        public Categoria()
        {
            this.Col_Producto = new HashSet<Producto>();
        }

        public Categoria(string categoriaNombre)
        {
            SetCoreData(categoriaNombre);
        }

        [Required]
        [Display(Name = "Categoría")]
        public string CategoriaNombre { get; private set; } = string.Empty;


        public void Update(string categoriaNombre)
        {
            SetCoreData(categoriaNombre);

        }

        public void Deactivate()
        {
            Touch();
        }

        private void SetCoreData(string categoriaNombre)
        {
            if (string.IsNullOrWhiteSpace(categoriaNombre))
                throw new ArgumentException("Nombre de Categoría es Requerido.");

            this.CategoriaNombre = categoriaNombre.Trim();
            this.FechaActualizacionUtc = DateTime.Now;
        }

        //Navegacion
        //Coleccion Entidades
        [Display(AutoGenerateField = false)]
        public virtual ICollection<Producto> Col_Producto { get; set; }

    }
}
