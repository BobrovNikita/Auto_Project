using Andasuk.Models;
using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.Interfaces
{
    public interface IProductView
    {
        Guid Id { get; set; }
        SpareViewModel SpareId { get; set; }
        CreatorViewModel CreatorId { get; set; }
        string PName { get; set; }
        string Description { get; set; }
        int Count { get; set; }
        int Cost { get; set; }


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

        void SetProductBindingSource(BindingSource source);
        void SetCreatorBindingSource(BindingSource source);
        void SetSpareBindingSource(BindingSource source);
        void Show();
    }
}
