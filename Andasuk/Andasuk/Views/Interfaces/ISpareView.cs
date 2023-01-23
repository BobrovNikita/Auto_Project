using Andasuk.Models;
using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.Interfaces
{
    public interface ISpareView
    {
        Guid Id { get; set; }
        CatalogViewModel CatalogId { get; set; }
        string SName { get; set; }

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

        void SetSpareBindingSource(BindingSource source);
        void SetCatalogBindingSource(BindingSource source);
        void Show();
    }
}
