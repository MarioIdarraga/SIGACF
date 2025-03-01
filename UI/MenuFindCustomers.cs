using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Contracts;
using DAL.Factory;
using Domain;

namespace UI
{
    public partial class MenuFindCustomers : Form
    {

        IGenericRepository<Customer> repositoryCustomer = Factory.Current.GetCustomerRepository();

        private Panel _panelContenedor;

        public MenuFindCustomers(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
        }

        private void OpenFormChild(object formchild)
        {
            if (_panelContenedor.Controls.Count > 0)
                _panelContenedor.Controls.RemoveAt(0);
            Form fh = formchild as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            _panelContenedor.Controls.Add(fh);
            _panelContenedor.Tag = fh;
            fh.Show();
        }

        private void btnRegCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegCustomer(_panelContenedor));
        }

        private void btnModCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModCustomer(_panelContenedor));
        }

        private void btnRegBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegBooking(_panelContenedor));
        }
    }
}
