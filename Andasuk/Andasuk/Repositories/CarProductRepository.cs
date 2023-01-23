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
    public class CarProductRepository : BaseRepository, IRepository<CarProductViewModel>
    {
        public CarProductRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CarProductViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new CarProduct();
                model.CarProductId= viewModel.CarProductId;
                model.CarId = viewModel.CarId;
                model.ProductId = viewModel.ProductId;

                new Common.ModelDataValidation().Validate(model);

                context.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(CarProductViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new CarProduct();
                model.CarProductId = viewModel.CarProductId;
                model.CarId = viewModel.CarId;
                model.ProductId = viewModel.ProductId;

                context.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<CarProductViewModel> GetAll()
        {
            return db.CarProducts.Include(o => o.Car).Include(o => o.Product).Select(o => new CarProductViewModel
            {
                CarProductId = o.CarProductId,
                CarId = o.CarId,
                ProductId = o.ProductId,
                CarName = o.Car.Model,
                ProductName = o.Product.Name
            }).ToList();
        }

        public IEnumerable<CarProductViewModel> GetAllByValue(string value)
        {
            var result = db.CarProducts.Include(o => o.Car).Include(o => o.Product).Where(o => o.Car.Model.Contains(value) ||
                                                                                               o.Product.Name.Contains(value));

            return result.Select(o => new CarProductViewModel
            {
                CarProductId = o.CarProductId,
                CarId = o.CarId,
                ProductId= o.ProductId,
                CarName = o.Car.Model,
                ProductName = o.Product.Name,
            }).ToList();
        }

        public CarProductViewModel GetModel(Guid id)
        {
            var result = db.CarProducts.Include(p => p.Car).Include(r => r.Product).First(c => c.CarProductId == id);

            var model = new CarProductViewModel();

            model.CarProductId = result.CarProductId;
            model.CarId = result.CarId;
            model.ProductId = result.ProductId;
            model.CarName = result.Car.Model;
            model.ProductName = result.Product.Name;

            return model;
        }

        public void Update(CarProductViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new CarProduct();
                model.CarProductId = viewModel.CarProductId;
                model.CarId = viewModel.CarId;
                model.ProductId = viewModel.ProductId;

                new Common.ModelDataValidation().Validate(model);

                context.Update(model);
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
