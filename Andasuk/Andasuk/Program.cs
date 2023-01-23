using Andasuk.Views.Interfaces;
using Andasuk.Views;
using Andasuk.Controllers;

namespace Andasuk
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            IMainView view = new MainView();
            new MainController(view);
            Application.Run((Form)view);
        }
    }
}