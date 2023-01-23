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
    public class ProductRepository : BaseRepository, IRepository<ProductViewModel>
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(ProductViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new Product();
                model.ProductId = viewModel.ProductId;
                model.Name = viewModel.PName;
                model.Description = viewModel.Description;
                model.Cost= viewModel.Cost;
                model.Count = viewModel.Count;
                model.CreatorId = viewModel.CreatorId;
                model.SpareId = viewModel.SpareId;

                new Common.ModelDataValidation().Validate(model);

                context.Products.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(ProductViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Product();
                model.ProductId = viewModel.ProductId;
                model.Name = viewModel.PName;
                model.Description = viewModel.Description;
                model.Cost = viewModel.Cost;
                model.Count = viewModel.Count;
                model.CreatorId = viewModel.CreatorId;

                context.Products.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return db.Products.Include(o => o.Creator).Include(o => o.Spare).Select(o => new ProductViewModel
            {
                ProductId = o.ProductId,
                PName = o.Name,
                Description = o.Description,
                Cost = o.Cost,
                Count = o.Count,
                SpareId = o.SpareId,
                CreatorId = o.CreatorId,
                CreatorName = o.Creator.Name,
                SpareName = o.Spare.Name
            }).ToList();
        }

        public IEnumerable<ProductViewModel> GetAllByValue(string value)
        {
            var result = db.Products.Include(o => o.Creator)
                                    .Include(o => o.Spare)
                                    .Where(o => o.Name.Contains(value) ||
                                                o.Description.Contains(value) ||
                                                o.Cost.ToString().Contains(value) ||
                                                o.Spare.Name.Contains(value) ||
                                                o.Creator.Name.Contains(value));
            return result.Select(o => new ProductViewModel
            {
                ProductId = o.ProductId,
                PName = o.Name,
                Description = o.Description,
                Cost = o.Cost,
                Count = o.Count,
                SpareId = o.SpareId,
                CreatorId = o.CreatorId,
                CreatorName = o.Creator.Name,
                SpareName = o.Spare.Name
            }).ToList();
        }

        public ProductViewModel GetModel(Guid id)
        {
            var result = db.Products.Include(p => p.Creator).Include(r => r.Spare).First(c => c.ProductId == id);

            var model = new ProductViewModel();

            model.ProductId = result.ProductId;
            model.PName = result.Name;
            model.Description = result.Description;
            model.Cost = result.Cost;
            model.Count = result.Count;
            model.SpareId = result.SpareId;
            model.CreatorId = result.CreatorId;
            model.CreatorName = result.Creator.Name;
            model.SpareName= result.Spare.Name;

            return model;
        }

        public void Update(ProductViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Product();
                model.ProductId = viewModel.ProductId;
                model.Name = viewModel.PName;
                model.Description = viewModel.Description;
                model.Cost = viewModel.Cost;
                model.Count = viewModel.Count;
                model.CreatorId = viewModel.CreatorId;
                model.SpareId= viewModel.SpareId;

                new Common.ModelDataValidation().Validate(model);

                context.Products.Update(model);
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
