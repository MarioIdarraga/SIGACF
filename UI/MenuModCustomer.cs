using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    public partial class MenuModCustomer : Form
    {
        private readonly CustomerSLService _customerSLService;

        private Panel _panelContenedor;

        private Guid _idCustomer;

        public MenuModCustomer(Panel panelContenedor, Guid idCustomer, int nroDocument, string firstName, string lastName, string comment,string telephone, string mail, string address, string state)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _idCustomer = idCustomer;

            this.Translate();

            var customerRepo = Factory.Current.GetCustomerRepository();
            var customerBLL = new CustomerService(customerRepo);

            var customerStateRepo = Factory.Current.GetCustomerStateRepository();
            var customerStateBLL = new CustomerStateService(customerStateRepo);

            _customerSLService = new CustomerSLService(customerBLL, customerStateBLL);

            txtNroDocument.Text = nroDocument.ToString();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtComment.Text = comment;
            txtTelephone.Text = telephone;
            txtMail.Text = mail;
            txtAddress.Text = address;
            txtState.Text = state;
        }

        public MenuModCustomer(Panel panelContenedor)
        {
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
            OpenFormChild(new MenuFindCustomers(_panelContenedor));
        }

        private void btnRegCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegCustomer(_panelContenedor));
        }

        private void btnModCustomer_Click(object sender, EventArgs e)
        {

            try
            {
                // Validar que un cliente ha sido seleccionado (se requiere un ID)
                if (string.IsNullOrWhiteSpace(txtNroDocument.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtState.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone.Text) || string.IsNullOrWhiteSpace(txtMail.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de modificar el cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear objeto Cliente con los datos del formulario
                Customer updatedCustomer = new Customer
                {
                    IdCustomer = _idCustomer, // Se pasa el ID del cliente seleccionado
                    NroDocument = int.Parse(txtNroDocument.Text),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    State = int.Parse(txtState.Text),
                    Comment = txtComment.Text,
                    Telephone = txtTelephone.Text,
                    Mail = txtMail.Text,
                    Address = txtAddress.Text
                };

                _customerSLService.Update(updatedCustomer.IdCustomer, updatedCustomer);

                MessageBox.Show("Cliente modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


