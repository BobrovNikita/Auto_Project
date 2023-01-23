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
    public class CarController
    {
        private readonly ICarView _view;
        private readonly IRepository<CarViewModel> _repository;

        private BindingSource carBindingSource;

        private IEnumerable<CarViewModel>? _cars;

        public CarController(ICarView view, IRepository<CarViewModel> repository)
        {
            _view = view;
            _repository = repository;

            carBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

            LoadCarList();

            view.SetCarBindingSource(carBindingSource);

            _view.Show();
        }


        private void LoadCarList()
        {
            _cars = _repository.GetAll();
            carBindingSource.DataSource = _cars;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.Mark = string.Empty;
            _view.Model = string.Empty;
            _view.DateProduction = DateTime.Now;
            _view.DatePO = DateTime.Now;
            _view.Capacity = string.Empty;
            _view.Power = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new CarViewModel();
            model.CarId = _view.Id;
            model.Mark = _view.Mark;
            model.CarModel = _view.Model;
            model.DateProduction = _view.DateProduction;
            model.DatePO = _view.DatePO;
            model.Capacity = _view.Capacity;
            model.Power = _view.Power;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Car edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Car added successfuly";
                }
                _view.IsSuccessful = true;
                LoadCarList();
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
                var model = (CarViewModel)carBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Catalog deleted successfuly";
                LoadCarList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Composition";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (CarViewModel)carBindingSource.Current;
            _view.Id = model.CarId;
            _view.Mark = model.Mark;
            _view.Model = model.CarModel;
            _view.DateProduction = model.DateProduction;
            _view.DatePO = model.DatePO;
            _view.Capacity = model.Capacity;
            _view.Power= model.Power;
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
                _cars = _repository.GetAllByValue(_view.searchValue);
            else
                _cars = _repository.GetAll();

            carBindingSource.DataSource = _cars;
        }

        private void Print(object? sender, EventArgs e)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(Application.StartupPath, Directory.GetCurrentDirectory() + "\\Автомобили.docx"));
            Word.Table tb = wordDocument.Tables[1];
            foreach (CarViewModel rw in carBindingSource)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.Mark.Trim();
                r.Cells[2].Range.Text = rw.CarModel.Trim();
                r.Cells[3].Range.Text = rw.Capacity.Trim();
                r.Cells[4].Range.Text = rw.DateProduction.ToString();
                r.Cells[5].Range.Text = rw.DatePO.ToString();
                r.Cells[6].Range.Text = rw.Power.ToString();
            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }
    }
}
