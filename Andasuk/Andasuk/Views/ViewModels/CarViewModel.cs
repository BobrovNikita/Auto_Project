using Andasuk.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.ViewModels
{
    public class CarViewModel
    {
        public Guid CarId { get; set; }

        [DisplayName("Марка")]
        public string Mark { get; set; }

        [DisplayName("Модель")]
        public string CarModel { get; set; }

        [DisplayName("Дата выпуска")]
        public DateTime DateProduction { get; set; }

        [DisplayName("Дата ПО")]
        public DateTime DatePO { get; set; }

        [DisplayName("Объем двигателя")]
        public string Capacity { get; set; }

        [DisplayName("Мощность")]
        public int Power { get; set; }
    }
}
