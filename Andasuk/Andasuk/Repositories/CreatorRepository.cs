using Andasuk.Models;
using Andasuk.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Repositories
{
    public class CreatorRepository : BaseRepository, IRepository<CreatorViewModel>
    {
        public CreatorRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CreatorViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new Creator();
                model.CreatorId = viewModel.CreatorId;
                model.Name= viewModel.Name;
                model.Country= viewModel.Country;
                model.UNP = viewModel.UNP;

                new Common.ModelDataValidation().Validate(model);

                context.Creators.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(CreatorViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Creator();
                model.CreatorId = viewModel.CreatorId;
                model.Name = viewModel.Name;
                model.Country = viewModel.Country;
                model.UNP = viewModel.UNP;

                context.Creators.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<CreatorViewModel> GetAll()
        {
            return db.Creators.Select(o => new CreatorViewModel
            {
                CreatorId = o.CreatorId,
                Name = o.Name,
                Country = o.Country,
                UNP = o.UNP
            }).ToList();
        }

        public IEnumerable<CreatorViewModel> GetAllByValue(string value)
        {
            var result = db.Creators.Where(o => o.Name.Contains(value) ||
                                                o.Country.Contains(value) ||
                                                o.UNP.Contains(value)
                                          );
            return result.Select(o => new CreatorViewModel
            {
                CreatorId = o.CreatorId,
                Name = o.Name,
                Country = o.Country,
                UNP = o.UNP
            }).ToList();
        }

        public CreatorViewModel GetModel(Guid id)
        {
            var result = db.Creators.First(s => s.CreatorId == id);

            var model = new CreatorViewModel();
            model.CreatorId = result.CreatorId;
            model.Name = result.Name;
            model.Country = result.Country;
            model.UNP = result.UNP;

            return model;
        }

        public void Update(CreatorViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Creator();
                model.CreatorId = viewModel.CreatorId;
                model.Name = viewModel.Name;
                model.Country = viewModel.Country;
                model.UNP = viewModel.UNP;

                new Common.ModelDataValidation().Validate(model);

                context.Creators.Update(model);
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
