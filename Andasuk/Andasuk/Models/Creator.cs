using Andasuk.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Models
{
    public class Creator
    {
        public Guid CreatorId { get; set; }

        [Required(ErrorMessage = "Name is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Country code is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Country code must be between 3 and 50")]
        public string Country { get; set; }

        [Required(ErrorMessage = "UNP is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "UNP code must be between 3 and 50")]
        public string UNP { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
