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

        [Required(ErrorMessage = "Name is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50")]
        public string Name { get; set; }

        public IEnumerable<Spare> Spares { get; set; }
    }
}
