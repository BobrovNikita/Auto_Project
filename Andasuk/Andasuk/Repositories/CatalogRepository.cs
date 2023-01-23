using Andasuk.Models;
using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Repositories
{
    public class CatalogRepository : BaseRepository, IRepository<CatalogViewModel>
    {
        public CatalogRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CatalogViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new Catalog();
                model.CatalogId= viewModel.CatalogId;
                model.Name = viewModel.Name;

                new Common.ModelDataValidation().Validate(model);

                context.Catalogs.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(CatalogViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Catalog();
                model.CatalogId = viewModel.CatalogId;
                model.Name = viewModel.Name;

                context.Catalogs.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<CatalogViewModel> GetAll()
        {
            return db.Catalogs.Select(o => new CatalogViewModel
            {
                CatalogId= o.CatalogId,
                Name= o.Name
            }).ToList();
        }

        public IEnumerable<CatalogViewModel> GetAllByValue(string value)
        {
            var result = db.Catalogs.Where(o => o.Name.Contains(value));

            return result.Select(o => new CatalogViewModel
            {
                CatalogId = o.CatalogId,
                Name = o.Name
            }).ToList();
        }

        public CatalogViewModel GetModel(Guid id)
        {
            var result = db.Catalogs.First(s => s.CatalogId == id);

            var model = new CatalogViewModel();
            model.CatalogId = result.CatalogId;
            model.Name = result.Name;

            return model;
        }

        public void Update(CatalogViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Catalog();
                model.CatalogId = viewModel.CatalogId;
                model.Name = viewModel.Name;

                new Common.ModelDataValidation().Validate(model);

                context.Catalogs.Update(model);
                context.SaveChanges();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
