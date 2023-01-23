using Andasuk.Repositories;
using Andasuk.Views.Interfaces;
using Andasuk.Views.ViewModels;
using Andasuk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Controllers
{
    public class MainController
    {
        private readonly IMainView _mainView;

        public MainController(IMainView mainView)
        {
            _mainView = mainView;

            _mainView.LoadProduct += LoadProduct;
            _mainView.LoadCar += LoadCar;
            _mainView.LoadCatalog += LoadCatalog;
            _mainView.LoadCreator += LoadCreator;
            _mainView.LoadSpare += LoadSpare;
            _mainView.LoadCarProduct += LoadCarProduct;
        }

        private void LoadCarProduct(object? sender, EventArgs e)
        {
            ICarProductView view = CarProductView.GetInstance((MainView)_mainView);
            IRepository<CarProductViewModel> repository = new CarProductRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            IRepository<CarViewModel> carRepository = new CarRepository(new ApplicationContext());
            new CarProductController(view, repository, productRepository, carRepository);
        }

        private void LoadSpare(object? sender, EventArgs e)
        {
            ISpareView view = SpareView.GetInstance((MainView)_mainView);
            IRepository<SpareViewModel> repository = new SpareRepository(new ApplicationContext());
            IRepository<CatalogViewModel> catalogRepository = new CatalogRepository(new ApplicationContext());
            new SpareController(view, repository, catalogRepository);
        }

        private void LoadCreator(object? sender, EventArgs e)
        {
            ICreatorView view = CreatorView.GetInstance((MainView)_mainView);
            IRepository<CreatorViewModel> repository = new CreatorRepository(new ApplicationContext());
            new CreatorController(view, repository);
        }

        private void LoadCatalog(object? sender, EventArgs e)
        {
            ICatalogView view = CatalogView.GetInstance((MainView)_mainView);
            IRepository<CatalogViewModel> repository = new CatalogRepository(new ApplicationContext());
            new CatalogController(view, repository);
        }

        private void LoadCar(object? sender, EventArgs e)
        {
            ICarView view = CarView.GetInstance((MainView)_mainView);
            IRepository<CarViewModel> repository = new CarRepository(new ApplicationContext());
            new CarController(view, repository);
        }

        private void LoadProduct(object? sender, EventArgs e)
        {
            IProductView view = ProductView.GetInstance((MainView)_mainView);
            IRepository<ProductViewModel> repository = new ProductRepository(new ApplicationContext());
            IRepository<CreatorViewModel> productRepository = new CreatorRepository(new ApplicationContext());
            IRepository<SpareViewModel> carRepository = new SpareRepository(new ApplicationContext());
            new ProductController(view, repository, productRepository, carRepository);
        }
    }
}
