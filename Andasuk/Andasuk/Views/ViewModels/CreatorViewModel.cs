using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.ViewModels
{
    public class CreatorViewModel
    {
        public Guid CreatorId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Страна")]
        public string Country { get; set; }

        [DisplayName("УНП")]
        public string UNP { get; set; }
    }
}
