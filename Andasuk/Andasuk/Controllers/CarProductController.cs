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
    public class CarProductController
    {
        private readonly ICarProductView _view;
        private readonly IRepository<CarProductViewModel> _repository;
        private readonly IRepository<ProductViewModel> _productRepository;
        private readonly IRepository<CarViewModel> _carRepository;

        private BindingSource carProductBindingSource;
        private BindingSource carBindingSource;
        private BindingSource productBindingSource;

        private IEnumerable<CarProductViewModel>? _carProducts;
        private IEnumerable<ProductViewModel>? _products;
        private IEnumerable<CarViewModel>? _cars;

        public CarProductController(ICarProductView view, IRepository<CarProductViewModel> repository, IRepository<ProductViewModel> productRepository, IRepository<CarViewModel> carRepository)
        {
            _view = view;
            _repository = repository;
            _productRepository = productRepository;
            _carRepository = carRepository;

            carProductBindingSource = new BindingSource();
            carBindingSource = new BindingSource();
            productBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

            LoadList();
            LoadCombobox();

            view.SetCarProductBindingSource(carProductBindingSource);
            view.SetCarBindingSource(carBindingSource);
            view.SetProductBindingSource(productBindingSource);

            _view.Show();
        }

        private void LoadList()
        {
            _carProducts = _repository.GetAll();
            carProductBindingSource.DataSource = _carProducts;
        }

        private void LoadCombobox()
        {
            _products = _productRepository.GetAll();
            productBindingSource.DataSource = _products;

            _cars = _carRepository.GetAll();
            carBindingSource.DataSource = _cars;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.CarId = new CarViewModel();
            _view.ProductId = new ProductViewModel();
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.CarId == null || _view.ProductId == null)
            {
                CleanViewFields();
                _view.Message = "Values are not specified in the combobox";
                return;
            }

            var model = new CarProductViewModel();
            model.CarProductId = _view.Id;
            model.CarId = _view.CarId.CarId;
            model.ProductId = _view.ProductId.ProductId;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Car product edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Car product added successfuly";
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
                var model = (CarProductViewModel)carProductBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Car product deleted successfuly";
                LoadList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Car product";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (CarProductViewModel)carProductBindingSource.Current;
            _view.Id = model.CarProductId;
            _view.CarId.CarId = model.CarId;
            _view.ProductId.ProductId = model.ProductId;
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
                _carProducts = _repository.GetAllByValue(_view.searchValue);
            else
                _carProducts = _repository.GetAll();

            carProductBindingSource.DataSource = _carProducts;
        }

        private void Print(object? sender, EventArgs e)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(Application.StartupPath, Directory.GetCurrentDirectory() + "\\АвтоПродукт.docx"));
            Word.Table tb = wordDocument.Tables[1];
            foreach (CarProductViewModel rw in carProductBindingSource)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.CarName.Trim();
                r.Cells[2].Range.Text = rw.ProductName.Trim();
            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }
    }
}
