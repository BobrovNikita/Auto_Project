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

        [Required(ErrorMessage = "Mark is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Mark must be between 3 and 50")]
        public string Mark { get; set; }


        [Required(ErrorMessage = "Model code is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Model code must be between 3 and 50")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Date Production is required field")]
        [CheckDate(ErrorMessage = "Date Production must be today or no more than 10 years")]
        public DateTime DateProduction { get; set; }

        [Required(ErrorMessage = "DatePO is required field")]
        [CheckDate(ErrorMessage = "DatePO must be today or no more than 10 years")]
        public DateTime DatePO { get; set; }

        [Required(ErrorMessage = "Capacity is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Capacity code must be between 3 and 50")]
        public string Capacity { get; set; }

        [Required(ErrorMessage = "Power is required field")]
        [Range(1, 1000, ErrorMessage = "Power must be between 1 and 1000")]
        public int Power { get; set; }

        public IEnumerable<CarProduct> CarProducts { get; set; }
    }
}
