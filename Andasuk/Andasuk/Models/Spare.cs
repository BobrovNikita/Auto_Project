using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Models
{
    public class Spare
    {
        public Guid SpareId { get; set; }

        [Required(ErrorMessage = "Название является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3-х до 50-и символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Каталог является обязательным полем")]
        public Guid CatalogId { get; set; }


        public Catalog Catalog { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
