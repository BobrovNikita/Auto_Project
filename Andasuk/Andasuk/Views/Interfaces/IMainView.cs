using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk.Views.Interfaces
{
    public interface IMainView
    {
        event EventHandler LoadCar;
        event EventHandler LoadCatalog;
        event EventHandler LoadSpare;
        event EventHandler LoadCreator;
        event EventHandler LoadProduct;
        event EventHandler LoadCarProduct;
    }
}
