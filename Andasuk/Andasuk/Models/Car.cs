using Andasuk.Common;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Models
{
    public class Car
    {
        public Guid CarId { get; set; }

        [Required(ErrorMessage = "Марка обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Марка должна быть от 3-х до 50-и символов")]
        public string Mark { get; set; }


        [Required(ErrorMessage = "Модель обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Модель должна быть от 3-х до 50-и символов")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Дата производства обязательное поле")]
        [CheckDate(ErrorMessage = "Дата производства должна быть в диапазоне от 01.01.2000 до 01.01.2050")]
        public DateTime DateProduction { get; set; }

        [Required(ErrorMessage = "Дата ПО обязательное поле")]
        [CheckDate(ErrorMessage = "Дата ПО должна быть в диапазоне от 01.01.2000 до 01.01.2050")]
        public DateTime DatePO { get; set; }

        [Required(ErrorMessage = "Объем двигателя обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Объем двигателя должен быть от 3-х до 50-и символов")]
        public string Capacity { get; set; }

        [Required(ErrorMessage = "Мощность двигателя обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Мощность двигателя должна быть в диапазоне от 1 до 1000")]
        public int Power { get; set; }

        public IEnumerable<CarProduct> CarProducts { get; set; }
    }
}
