using Andasuk.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Andasuk.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            InitializeBtnEvents();
        }

        private void InitializeBtnEvents()
        {
            CarBtn.Click += delegate { LoadCar?.Invoke(this, EventArgs.Empty); };
            ProductBtn.Click += delegate { LoadProduct?.Invoke(this, EventArgs.Empty); };
            CatalogBtn.Click += delegate { LoadCatalog?.Invoke(this, EventArgs.Empty); };
            SpareBtn.Click += delegate { LoadSpare?.Invoke(this, EventArgs.Empty); };
            CreatorBtn.Click += delegate { LoadCreator?.Invoke(this, EventArgs.Empty); };
            CarProductBtn.Click += delegate { LoadCarProduct?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler LoadCar;
        public event EventHandler LoadCatalog;
        public event EventHandler LoadSpare;
        public event EventHandler LoadCreator;
        public event EventHandler LoadProduct;
        public event EventHandler LoadCarProduct;
    }
}
