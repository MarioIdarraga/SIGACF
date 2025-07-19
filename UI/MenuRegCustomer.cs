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
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using UI.Helpers;


namespace UI
{
    public partial class MenuRegCustomer : Form
    {
        private Panel _panelContenedor;

        private readonly CustomerSLService _customerSLService;

        public MenuRegCustomer(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); 
            var repo = Factory.Current.GetCustomerRepository();
            var bllService = new CustomerService(repo);
            _customerSLService = new CustomerSLService(bllService);
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
            OpenFormChild(new MenuFindCustomers(_panelContenedor));
        }

        private void btnModCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModCustomer(_panelContenedor));
        }

        private void btnRegCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNroDocument.Text) || !int.TryParse(txtNroDocument.Text, out int nroDocument))
                {
                    MessageBox.Show("El número de documento debe ser un valor válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Customer newCustomer = new Customer
                {
                    NroDocument = nroDocument,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    State = 0,
                    Comment = txtComment.Text,
                    Telephone = txtTelephone.Text,
                    Mail = txtMail.Text,
                    Address = txtAddress.Text
                };

                _customerSLService.Insert(newCustomer);

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
            txtComment.Text = "";
            txtTelephone.Text = "";
            txtMail.Text = "";
            txtAddress.Text = "";
        }
    }
}

