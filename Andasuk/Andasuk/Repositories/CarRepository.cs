using Andasuk.Models;
using Andasuk.Views.ViewModels;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Repositories
{
    public class CarRepository : BaseRepository, IRepository<CarViewModel>
    {
        public CarRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CarViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                Car model = new Car();
                model.CarId= viewModel.CarId;
                model.Mark = viewModel.Mark;
                model.Model = viewModel.CarModel;
                model.DateProduction= viewModel.DateProduction;
                model.DatePO = viewModel.DatePO;
                model.Capacity = viewModel.Capacity;
                model.Power = viewModel.Power;

                new Common.ModelDataValidation().Validate(model);

                context.Cars.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(CarViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new Car();
                model.CarId= viewModel.CarId;
                model.Mark = viewModel.Mark;
                model.Model = viewModel.CarModel;
                model.DateProduction= viewModel.DateProduction;
                model.DatePO = viewModel.DatePO;
                model.Capacity = viewModel.Capacity;
                model.Power = viewModel.Power;

                context.Cars.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<CarViewModel> GetAll()
        {
            return db.Cars.Select(o => new CarViewModel
            {
                CarId= o.CarId,
                Mark = o.Mark,
                CarModel = o.Model,
                DateProduction= o.DateProduction,
                DatePO= o.DatePO,
                Capacity= o.Capacity,
                Power = o.Power

            }).ToList();
        }

        public IEnumerable<CarViewModel> GetAllByValue(string value)
        {
            var result = db.Cars.Where(o => o.Mark.Contains(value) ||
                                            o.Model.Contains(value) ||
                                            o.Capacity.Contains(value) ||
                                            o.Power.ToString().Contains(value) ||
                                            o.DateProduction.ToString().Contains(value) ||
                                            o.DatePO.ToString().Contains(value)
                                            );
            return result.Select(o => new CarViewModel
            {
                CarId = o.CarId,
                Mark = o.Mark,
                CarModel = o.Model,
                DateProduction = o.DateProduction,
                DatePO = o.DatePO,
                Capacity = o.Capacity,
                Power = o.Power
            }).ToList();
        }

        public CarViewModel GetModel(Guid id)
        {
            var result = db.Cars.First(s => s.CarId == id);

            var model = new CarViewModel();
            model.CarId= result.CarId;
            model.CarModel = result.Model;
            model.Capacity = result.Capacity;
            model.Mark = result.Mark;
            model.Power= result.Power;
            model.DateProduction = result.DateProduction;
            model.DatePO = result.DatePO;

            return model;
        }

        public void Update(CarViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new Car();
                model.CarId = viewModel.CarId;
                model.Mark = viewModel.Mark;
                model.Model = viewModel.CarModel;
                model.DateProduction = viewModel.DateProduction;
                model.DatePO = viewModel.DatePO;
                model.Capacity = viewModel.Capacity;
                model.Power = viewModel.Power;

                new Common.ModelDataValidation().Validate(model);

                context.Cars.Update(model);
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
