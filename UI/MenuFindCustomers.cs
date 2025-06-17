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
using UI.Helpers;

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
            this.Translate(); // Assuming you have a Translate method for localization
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
            try
            {
                // Validar si hay filas en el DataGridView
                if (dataGridViewCustomers.Rows.Count == 0)
                {
                    MessageBox.Show("Debe de seleccionar un cliente de la lista para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar si hay una fila seleccionada
                if (dataGridViewCustomers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un cliente para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener datos de la fila seleccionada
                DataGridViewRow selectedRow = dataGridViewCustomers.SelectedRows[0];

                // Validar que las celdas no sean nulas
                if (selectedRow.Cells["IdCustomer"].Value == null ||
                    selectedRow.Cells["NroDocument"].Value == null ||
                    selectedRow.Cells["FirstName"].Value == null ||
                    selectedRow.Cells["LastName"].Value == null ||
                    selectedRow.Cells["Telephone"].Value == null ||
                    selectedRow.Cells["Mail"].Value == null)
                {
                    MessageBox.Show("El cliente seleccionado tiene datos inválidos. Intente seleccionar otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extraer valores de la fila
                Guid idCustomer = (Guid)selectedRow.Cells["IdCustomer"].Value;
                int nroDocument = Convert.ToInt32(selectedRow.Cells["NroDocument"].Value);
                string firstName = selectedRow.Cells["FirstName"].Value.ToString();
                string lastName = selectedRow.Cells["LastName"].Value.ToString();
                string comment = selectedRow.Cells["Comment"].Value.ToString();
                string telephone = selectedRow.Cells["Telephone"].Value.ToString();
                string mail = selectedRow.Cells["Mail"].Value.ToString();
                string address = selectedRow.Cells["Address"].Value.ToString();
                string state = selectedRow.Cells["State"].Value.ToString();

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModCustomer(_panelContenedor, idCustomer, nroDocument, firstName, lastName, comment, telephone, mail, address, state));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar modificar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si hay filas en el DataGridView
                if (dataGridViewCustomers.Rows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un cliente de la lista para registrar una reserva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar si hay una fila seleccionada
                if (dataGridViewCustomers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un cliente para registrar una reserva.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener datos de la fila seleccionada
                DataGridViewRow selectedRow = dataGridViewCustomers.SelectedRows[0];

                // Validar que la celda de IdCustomer no sea nula
                if (selectedRow.Cells["IdCustomer"].Value == null)
                {
                    MessageBox.Show("El cliente seleccionado tiene datos inválidos. Intente seleccionar otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extraer el IdCustomer
                Guid idCustomer = (Guid)selectedRow.Cells["IdCustomer"].Value;
                string nroDocument = selectedRow.Cells["NroDocument"].Value.ToString();

                OpenFormChild(new MenuRegBooking(_panelContenedor, idCustomer, nroDocument));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar registrar la reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            
            int? nroDocumento = null;
            if (!string.IsNullOrWhiteSpace(txtNroDocument.Text))
            {
                if (int.TryParse(txtNroDocument.Text, out int result))
                {
                    nroDocumento = result;
                }
                else
                {
                    MessageBox.Show("Ingrese un número de documento válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string firstName = string.IsNullOrWhiteSpace(txtFirstName.Text) ? null : txtFirstName.Text.Trim();
            string lastName = string.IsNullOrWhiteSpace(txtLastName.Text) ? null : txtLastName.Text.Trim();
            string telephone = string.IsNullOrWhiteSpace(txtTelephone.Text) ? null : txtTelephone.Text.Trim();
            string mail = string.IsNullOrWhiteSpace(txtMail.Text) ? null : txtMail.Text.Trim();

            try
            {
                // Llamada a la DAL
                var customers = repositoryCustomer.GetAll(nroDocumento, firstName, lastName, telephone, mail);

                // Mostrar resultados en un DataGridView
                dataGridViewCustomers.DataSource = customers.ToList();

                // Mensaje en la UI
                lblStatus.Text = customers.Any()
                    ? $"Se encontraron {customers.Count()} clientes."
                    : "No se encontraron clientes con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
