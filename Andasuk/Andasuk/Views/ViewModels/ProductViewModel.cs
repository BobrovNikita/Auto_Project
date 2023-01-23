using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid SpareId { get; set; }


        [DisplayName("Название")]
        public string PName { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }


        [DisplayName("Остаток")]
        public int Count { get; set; }

        [DisplayName("Цена")]
        public int Cost { get; set; }

        [DisplayName("Запчасть")]
        public string SpareName { get; set; }

        [DisplayName("Производитель")]

        public string CreatorName { get; set; }

        
    }
}
