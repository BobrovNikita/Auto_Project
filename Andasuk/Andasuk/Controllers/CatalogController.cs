using Andasuk.Repositories;
using Andasuk.Views.Interfaces;
using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Andasuk.Controllers
{
    public class CatalogController
    {
        private readonly ICatalogView _view;
        private readonly IRepository<CatalogViewModel> _repository;

        private BindingSource catalogBindingSource;

        private IEnumerable<CatalogViewModel>? _catalogs;

        public CatalogController(ICatalogView view, IRepository<CatalogViewModel> repository)
        {
            _view = view;
            _repository = repository;

            catalogBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

            LoadProductTypeList();

            view.SetCatalogBindingSource(catalogBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _catalogs = _repository.GetAll();
            catalogBindingSource.DataSource = _catalogs;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.CName = string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new CatalogViewModel();
            model.CatalogId = _view.Id;
            model.Name = _view.CName;           

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Catalog edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Catalog added successfuly";
                }
                _view.IsSuccessful = true;
                LoadProductTypeList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                _view.IsSuccessful = false;
                _view.Message = ex.Message;
            }
        }

        private void DeleteSelected(object? sender, EventArgs e)
        {
            try
            {
                var model = (CatalogViewModel)catalogBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Catalog deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Composition";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (CatalogViewModel)catalogBindingSource.Current;
            _view.Id = model.CatalogId;
            _view.CName = model.Name;
            _view.IsEdit = true;
        }

        private void Add(object? sender, EventArgs e)
        {
            _view.IsEdit = false;
        }

        private void Search(object? sender, EventArgs e)
        {
            bool emptyValue = String.IsNullOrWhiteSpace(_view.searchValue);

            if (emptyValue == false)
                _catalogs = _repository.GetAllByValue(_view.searchValue);
            else
                _catalogs = _repository.GetAll();

            catalogBindingSource.DataSource = _catalogs;
        }

        private void Print(object? sender, EventArgs e)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(Application.StartupPath, Directory.GetCurrentDirectory() + "\\Каталоги.docx"));
            Word.Table tb = wordDocument.Tables[1];
            foreach (CatalogViewModel rw in catalogBindingSource)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.Name.Trim();
            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }
    }
}
