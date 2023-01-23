using Andasuk.Models;
using Andasuk.Views.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Repositories
{
    public class SpareRepository : BaseRepository, IRepository<SpareViewModel>
    {
        public SpareRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(SpareViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new Spare();
                model.SpareId = viewModel.SpareId;
                model.Name = viewModel.Name;
                model.CatalogId = viewModel.CatalogId;

                new Common.ModelDataValidation().Validate(model);

                context.Spares.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(SpareViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Spare();
                model.SpareId = viewModel.SpareId;
                model.Name = viewModel.Name;
                model.CatalogId = viewModel.CatalogId;

                context.Spares.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<SpareViewModel> GetAll()
        {
            return db.Spares.Include(o => o.Catalog).Select(o => new SpareViewModel
            {
                SpareId = o.SpareId,
                Name = o.Name,
                CatalogId = o.CatalogId,
                CatalogName = o.Catalog.Name
            }).ToList();
        }

        public IEnumerable<SpareViewModel> GetAllByValue(string value)
        {
            var result = db.Spares.Include(o => o.Catalog).Where(o => o.Name.Contains(value) ||
                                                                      o.Catalog.Name.Contains(value));

            return result.Select(o => new SpareViewModel
            {
                SpareId = o.SpareId,
                Name = o.Name,
                CatalogName = o.Catalog.Name
            }).ToList();
        }

        public SpareViewModel GetModel(Guid id)
        {
            var result = db.Spares.Include(p => p.Catalog).First(c => c.SpareId == id);

            var model = new SpareViewModel();
            model.SpareId = result.SpareId;
            model.Name = result.Name;
            model.CatalogId = result.CatalogId;
            model.CatalogName = result.Catalog.Name;

            return model;
        }

        public void Update(SpareViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Spare();
                model.SpareId = viewModel.SpareId;
                model.Name = viewModel.Name;
                model.CatalogId = viewModel.CatalogId;

                new Common.ModelDataValidation().Validate(model);

                context.Spares.Update(model);
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
