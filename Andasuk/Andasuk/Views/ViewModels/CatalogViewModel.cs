using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.ViewModels
{
    public class CatalogViewModel
    {
        public Guid CatalogId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
    }
}
