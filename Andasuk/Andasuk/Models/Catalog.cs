using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Models
{
    public class Catalog
    {
        public Guid CatalogId { get; set; }

        [Required(ErrorMessage = "Название является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3-х до 50-и символов")]
        public string Name { get; set; }

        public IEnumerable<Spare> Spares { get; set; }
    }
}
