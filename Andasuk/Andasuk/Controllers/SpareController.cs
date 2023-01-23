using Andasuk.Repositories;
using Andasuk.Views;
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
    public class SpareController
    {
        private readonly ISpareView _view;
        private readonly IRepository<SpareViewModel> _repository;
        private readonly IRepository<CatalogViewModel> _catalogRepository;

        private BindingSource spareBindingSource;
        private BindingSource catalogBindingSource;

        private IEnumerable<SpareViewModel>? _spares;
        private IEnumerable<CatalogViewModel>? _catalogs;

        public SpareController(ISpareView view, IRepository<SpareViewModel> repository, IRepository<CatalogViewModel> catalogRepository)
        {
            _view = view;
            _repository = repository;
            _catalogRepository = catalogRepository;

            spareBindingSource = new BindingSource();
            catalogBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

            LoadProductTypeList();
            LoadCombobox();

            view.SetSpareBindingSource(spareBindingSource);
            view.SetCatalogBindingSource(catalogBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _spares = _repository.GetAll();
            spareBindingSource.DataSource = _spares;
        }

        private void LoadCombobox()
        {
            _catalogs = _catalogRepository.GetAll();
            catalogBindingSource.DataSource = _catalogs;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.SName= string.Empty;
            _view.CatalogId = new CatalogViewModel();
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.CatalogId == null)
            {
                CleanViewFields();
                _view.Message = "Values are not specified in the combobox";
                return;
            }

            var model = new SpareViewModel();
            model.SpareId = _view.Id;
            model.CatalogId = _view.CatalogId.CatalogId;
            model.Name = _view.SName;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Spare edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Spare added successfuly";
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
                var model = (SpareViewModel)spareBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Spare deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Spare";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (SpareViewModel)spareBindingSource.Current;
            _view.Id = model.SpareId;
            _view.CatalogId.CatalogId = model.CatalogId;
            _view.SName = model.Name;
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
                _spares = _repository.GetAllByValue(_view.searchValue);
            else
                _spares = _repository.GetAll();

            spareBindingSource.DataSource = _spares;
        }

        private void Print(object? sender, EventArgs e)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(Application.StartupPath, Directory.GetCurrentDirectory() + "\\Запчасти.docx"));
            Word.Table tb = wordDocument.Tables[1];
            foreach (SpareViewModel rw in spareBindingSource)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.Name.Trim();
                r.Cells[2].Range.Text = rw.CatalogName.Trim();
            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }
    }
}
