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
    public class CreatorController
    {
        private readonly ICreatorView _view;
        private readonly IRepository<CreatorViewModel> _repository;

        private BindingSource creatorBindingSource;

        private IEnumerable<CreatorViewModel>? _creators;

        public CreatorController(ICreatorView view, IRepository<CreatorViewModel> repository)
        {
            _view = view;
            _repository = repository;

            creatorBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

            LoadList();

            view.SetCreatorBindingSource(creatorBindingSource);

            _view.Show();
        }

        private void LoadList()
        {
            _creators = _repository.GetAll();
            creatorBindingSource.DataSource = _creators;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.SName= string.Empty;
            _view.Country = string.Empty;
            _view.UNP= string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new CreatorViewModel();
            model.CreatorId = _view.Id;
            model.Name = _view.SName;
            model.Country = _view.Country;
            model.UNP = _view.UNP;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Creator edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Creator added successfuly";
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
                var model = (CreatorViewModel)creatorBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Creator deleted successfuly";
                LoadList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Creator";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (CreatorViewModel)creatorBindingSource.Current;
            _view.Id = model.CreatorId;
            _view.SName= model.Name;
            _view.Country= model.Country;
            _view.UNP = model.UNP;
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
                _creators = _repository.GetAllByValue(_view.searchValue);
            else
                _creators = _repository.GetAll();

            creatorBindingSource.DataSource = _creators;
        }

        private void Print(object? sender, EventArgs e)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(Application.StartupPath, Directory.GetCurrentDirectory() + "\\Производитель.docx"));
            Word.Table tb = wordDocument.Tables[1];
            foreach (CreatorViewModel rw in creatorBindingSource)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.Name.Trim();
                r.Cells[2].Range.Text = rw.Country.Trim();
                r.Cells[3].Range.Text = rw.UNP.Trim();
            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }
    }
}
