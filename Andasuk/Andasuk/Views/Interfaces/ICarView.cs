using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.Interfaces
{
    public interface ICarView
    {
        Guid Id { get; set; }
        string Mark { get; set; }
        string Model { get; set; }
        DateTime DateProduction { get; set; }
        DateTime DatePO { get; set; }
        string Capacity { get; set; }
        int Power { get; set; }


        string searchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler PrintEvent;

        void SetCarBindingSource(BindingSource source);
        void Show();
    }
}
