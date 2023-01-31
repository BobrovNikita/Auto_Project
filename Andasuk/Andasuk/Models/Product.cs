using Andasuk.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Название является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3-х до 50-и символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описание является обязательным полем")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Описание должно быть от 3-х до 50-и символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Количество является обязательным полем")]
        [Range(1, 10000, ErrorMessage = "Количество должно быть от 3-х до 50-и символов")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Цена является обязательным полем")]
        [Range(1, 10000, ErrorMessage = "Цена является должна быть от 3-х до 50-и символов")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Производитель является обязательным полем")]
        public Guid CreatorId { get; set; }

        [Required(ErrorMessage = "Деталь является обязательным полем")]
        public Guid SpareId { get; set; }

        public Spare Spare { get; set; }
        public Creator Creator { get; set; }

        public IEnumerable<CarProduct> CarProducts { get; set; }


    }
}
