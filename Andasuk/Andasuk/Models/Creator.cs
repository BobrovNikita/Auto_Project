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

        [Required(ErrorMessage = "Название является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3-х до 50-и символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Страна является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Страна должна быть от 3-х до 50-и символов")]
        public string Country { get; set; }

        [Required(ErrorMessage = "УНП является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "УНП должно быть от 3-х до 50-и символов")]
        public string UNP { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
