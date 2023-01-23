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
    public class ProductController
    {
        private readonly IProductView _view;
        private readonly IRepository<ProductViewModel> _repository;
        private readonly IRepository<CreatorViewModel> _creatorRepository;
        private readonly IRepository<SpareViewModel> _spareRepository;

        private BindingSource productBindingSource;
        private BindingSource creatorBindingSource;
        private BindingSource spareBindingSource;

        private IEnumerable<ProductViewModel>? _products;
        private IEnumerable<CreatorViewModel>? _creators;
        private IEnumerable<SpareViewModel>? _spares;

        public ProductController(IProductView view, IRepository<ProductViewModel> repository, IRepository<CreatorViewModel> creatorRepository, IRepository<SpareViewModel> spareRepository)
        {
            _view = view;
            _repository = repository;
            _creatorRepository = creatorRepository;
            _spareRepository = spareRepository; 

            productBindingSource = new BindingSource();
            creatorBindingSource = new BindingSource();
            spareBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

            LoadList();
            LoadCombobox();

            view.SetProductBindingSource(productBindingSource);
            view.SetCreatorBindingSource(creatorBindingSource);
            view.SetSpareBindingSource(spareBindingSource);

            _view.Show();
        }

        private void LoadList()
        {
            _products = _repository.GetAll();
            productBindingSource.DataSource = _products;
        }

        private void LoadCombobox()
        {
            _creators = _creatorRepository.GetAll();
            creatorBindingSource.DataSource = _creators;

            _spares = _spareRepository.GetAll();
            spareBindingSource.DataSource = _spares;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.SpareId = new SpareViewModel();
            _view.CreatorId = new CreatorViewModel();
            _view.PName = string.Empty;
            _view.Description = string.Empty;
            _view.Cost = -1;
            _view.Count = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.SpareId == null || _view.CreatorId == null)
            {
                CleanViewFields();
                _view.Message = "Values are not specified in the combobox";
                return;
            }

            var model = new ProductViewModel();

            model.ProductId = _view.Id;
            model.SpareId = _view.SpareId.SpareId;
            model.CreatorId = _view.CreatorId.CreatorId;
            model.PName = _view.PName;
            model.Description = _view.Description;
            model.Cost = _view.Cost;
            model.Count = _view.Count;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Product edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Product added successfuly";
                }
                _view.IsSuccessful = true;
                LoadList();
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
                var model = (ProductViewModel)productBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Product deleted successfuly";
                LoadList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Product";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (ProductViewModel)productBindingSource.Current;
            _view.Id = model.ProductId;
            _view.SpareId.SpareId = model.SpareId;
            _view.CreatorId.CreatorId = model.CreatorId;
            _view.PName = model.PName;
            _view.Description = model.Description;
            _view.Cost = model.Cost;
            _view.Count= model.Count;
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
                _products = _repository.GetAllByValue(_view.searchValue);
            else
                _products = _repository.GetAll();

            productBindingSource.DataSource = _products;
        }

        private void Print(object? sender, EventArgs e)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(Application.StartupPath, Directory.GetCurrentDirectory() + "\\Продукты.docx"));
            Word.Table tb = wordDocument.Tables[1];
            foreach (ProductViewModel rw in productBindingSource)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.PName.Trim();
                r.Cells[2].Range.Text = rw.Description.Trim();
                r.Cells[3].Range.Text = rw.Count.ToString();
                r.Cells[4].Range.Text = rw.Cost.ToString();
                r.Cells[5].Range.Text = rw.CreatorName.Trim();
                r.Cells[6].Range.Text = rw.SpareName.Trim();
            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }
    }
}
