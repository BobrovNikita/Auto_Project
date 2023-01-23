using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.ViewModels
{
    public class CarProductViewModel
    {
        public Guid CarProductId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CarId { get; set; }

        [DisplayName("Продукт")]
        public string ProductName { get; set; }

        [DisplayName("Автомобиль")]
        public string CarName { get; set; }
    }
}
