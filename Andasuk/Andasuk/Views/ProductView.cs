using Andasuk.Views.Interfaces;
using Andasuk.Views.ViewModels;
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
    public partial class ProductView : Form, IProductView
    {
        private string? _message;
        private bool _isSuccessful;
        private bool _isEdit;

        public Guid Id
        {
            get => Guid.Parse(IdTxt.Text);
            set => IdTxt.Text = value.ToString();
        }
        public SpareViewModel SpareId
        {
            get => (SpareViewModel)SpareCmb.SelectedItem;
            set => SpareCmb.SelectedItem = value;
        }
        public CreatorViewModel CreatorId
        {
            get => (CreatorViewModel)CreatorCmb.SelectedItem;
            set => CreatorCmb.SelectedItem = value;
        }
        public string PName
        {
            get => CNameTxt.Text;
            set => CNameTxt.Text = value;
        }
        public string Description
        {
            get => DescriptionTxt.Text;
            set => DescriptionTxt.Text = value;
        }
        public int Count
        {
            get
            {
                if (!int.TryParse(CountTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(CountTxt.Text);
                }

            }
            set
            {
                if (value != -1)
                {
                    CountTxt.Text = value.ToString();
                }
                else
                    CountTxt.Text = string.Empty;
            }
        }
        public int Cost
        {
            get
            {
                if (!int.TryParse(CostTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(CostTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    CostTxt.Text = value.ToString();
                }
                else
                    CostTxt.Text = string.Empty;
            }
        }
        public string searchValue
        {
            get => SearchTxt.Text;
            set => SearchTxt.Text = value;
        }
        public bool IsEdit
        {
            get => _isEdit;
            set => _isEdit = value;
        }
        public bool IsSuccessful
        {
            get => _isSuccessful;
            set => _isSuccessful = value;
        }
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler PrintEvent;

        public ProductView()
        {
            InitializeComponent();
            AssosiateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            CloseBtn.Click += delegate { this.Close(); };
            IdTxt.Text = Guid.Empty.ToString();
        }
        private void AssosiateAndRaiseViewEvents()
        {
            //Search
            SearchBtn.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            SearchTxt.KeyDown += (s, e) =>
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };

            //Add new
            AddBtn.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.TabPages.Remove(tabPage1);
                tabPage2.Text = "Добавление";
            };

            //Edit
            EditBtn.Click += delegate
            {
                if (dataGridView1.Rows.Count >= 1)
                {
                    tabControl1.TabPages.Remove(tabPage1);
                    tabControl1.TabPages.Add(tabPage2);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    tabPage2.Text = "Редактирование";
                }
                else
                {
                    MessageBox.Show("You didn't choose some redord");
                }
            };

            //Delete
            DeleteBtn.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected record", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            //Save 
            SaveBtn.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Add(tabPage1);
                    tabControl1.TabPages.Remove(tabPage2);
                }

                MessageBox.Show(Message);
            };

            //Cancel
            CancelBtn.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
            };

            CountTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            CostTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            PrintBtn.Click += delegate
            {
                PrintEvent?.Invoke(this, EventArgs.Empty);
            };
        }
        public void SetProductBindingSource(BindingSource source)
        {
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }

        public void SetCreatorBindingSource(BindingSource source)
        {
            CreatorCmb.DataSource = source;
            CreatorCmb.DisplayMember = "Name";
            CreatorCmb.ValueMember = "Id";
        }

        public void SetSpareBindingSource(BindingSource source)
        {
            SpareCmb.DataSource = source;
            SpareCmb.DisplayMember = "Name";
            SpareCmb.ValueMember = "Id";
        }

        private static ProductView? instance;

        public static ProductView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new ProductView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;

                instance.BringToFront();
            }

            return instance;
        }
    }
}
