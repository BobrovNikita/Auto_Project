using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Models
{
    public class CarProduct
    {
        public Guid CarProductId { get; set; }

        [Required(ErrorMessage = "Product ID is required field")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Car ID is required field")]
        public Guid CarId { get; set; }

        public Car Car { get; set; }
        public Product Product { get; set; }

    }
}
