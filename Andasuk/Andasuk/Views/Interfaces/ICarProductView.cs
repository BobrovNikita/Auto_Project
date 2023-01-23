using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.Interfaces
{
    public interface ICarProductView
    {
        Guid Id { get; set; }
        CarViewModel CarId { get; set; }
        ProductViewModel ProductId { get; set; }

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

        void SetCarProductBindingSource(BindingSource source);
        void SetProductBindingSource(BindingSource source);
        void SetCarBindingSource(BindingSource source);
        void Show();
    }
}
