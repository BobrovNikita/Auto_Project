using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.ViewModels
{
    public class SpareViewModel
    {
        public Guid SpareId { get; set; }
        public Guid CatalogId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Каталог")]
        public string CatalogName { get; set; }
    }
}
