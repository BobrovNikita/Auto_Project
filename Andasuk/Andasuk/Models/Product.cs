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

        [Required(ErrorMessage = "Name is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 50")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Count is required field")]
        [Range(1, 10000, ErrorMessage = "Count must be between 1 and 10000")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Cost is required field")]
        [Range(1, 10000, ErrorMessage = "Cost must be between 1 and 10000")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Creator ID is required field")]
        public Guid CreatorId { get; set; }

        [Required(ErrorMessage = "Shop ID is required field")]
        public Guid SpareId { get; set; }

        public Spare Spare { get; set; }
        public Creator Creator { get; set; }

        public IEnumerable<CarProduct> CarProducts { get; set; }


    }
}
