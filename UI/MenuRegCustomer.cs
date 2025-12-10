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
using SL.Service;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para registrar nuevos clientes en el sistema.
    /// Permite ingresar datos básicos del cliente y acceder a la búsqueda y modificación.
    /// </summary>
    public partial class MenuRegCustomer : Form
    {
        private Panel _panelContenedor;
        private readonly CustomerSLService _customerSLService;

        /// <summary>
        /// Inicializa una nueva instancia del formulario MenuRegCustomer.
        /// Configura traducción, repositorios, servicios y dependencias necesarias.
        /// </summary>
        /// <param name="panelContenedor">Panel padre donde se cargarán los formularios hijos.</param>
        public MenuRegCustomer(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var customerRepo = Factory.Current.GetCustomerRepository();
            var customerBLL = new CustomerService(customerRepo);

            var customerStateRepo = Factory.Current.GetCustomerStateRepository();
            var customerStateBLL = new CustomerStateService(customerStateRepo);

            _customerSLService = new CustomerSLService(customerBLL, customerStateBLL);
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
        /// <param name="formchild">Instancia del formulario hijo que será mostrado.</param>
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

        /// <summary>
        /// Abre el formulario de búsqueda de clientes.
        /// </summary>
        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindCustomers(_panelContenedor));
        }

        /// <summary>
        /// Abre el formulario de modificación de clientes existentes.
        /// </summary>
        private void btnModCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModCustomer(_panelContenedor));
        }

        /// <summary>
        /// Valida los datos ingresados y registra un nuevo cliente mediante CustomerSLService.
        /// </summary>
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
                    State = 1,
                    Comment = txtComment.Text,
                    Telephone = txtTelephone.Text,
                    Mail = txtMail.Text,
                    Address = txtAddress.Text
                };

                _customerSLService.Insert(newCustomer);

                MessageBox.Show("El Cliente se ha Registrado Correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia los campos del formulario luego de un registro exitoso.
        /// </summary>
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
