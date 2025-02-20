using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Contracts;
using DAL.Factory;
using Domain;


namespace UI
{
    public partial class MenuRegCustomer : Form
    {
        private Panel _panelContenedor;

        IGenericRepository<Customer> repositoryCustomer = Factory.Current.GetCustomerRepository();
        public MenuRegCustomer(Panel panelContenedor)
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

        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuCustomers(_panelContenedor));
        }

        private void btnModCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModCustomer(_panelContenedor));
        }

        private void btnRegCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer
                {
                    NroDocument = int.Parse(txtNroDocument.Text),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    State = int.Parse(txtState.Text),
                    Comment = txtComment.Text,
                    Telephone = txtTelephone.Text,
                    Mail = txtMail.Text,
                    Address = txtAddress.Text
                };

                repositoryCustomer.Insert(newCustomer);

                MessageBox.Show("Cliente guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCampos()
        {
            txtNroDocument.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtState.Text = "";
            txtComment.Text = "";
            txtTelephone.Text = "";
            txtMail.Text = "";
            txtAddress.Text = "";
        }
    }
}

