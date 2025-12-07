using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using UI.Helpers;
using BLL.BusinessException;
using SL.Service;
using SL.BLL;

namespace UI
{
    /// <summary>
    /// Formulario de búsqueda y gestión de clientes.
    /// Permite filtrar clientes, registrar nuevos, modificarlos
    /// y registrar reservas asociadas a un cliente seleccionado.
    /// </summary>
    public partial class MenuFindCustomers : Form
    {
        private Panel _panelContenedor;

        private readonly CustomerSLService _customerSLService;

        private readonly CustomerStateSLService _customerStateSLService;

        /// <summary>
        /// Constructor del formulario de búsqueda de clientes y de Estado de Clientes.
        /// Inicializa el panel contenedor, traduce el formulario
        /// e instancia el servicio de clientes de la capa SL.
        /// </summary>
        /// <param name="panelContenedor">
        /// Panel principal sobre el que se cargarán los formularios hijos.
        /// </param>
        public MenuFindCustomers(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var repo = Factory.Current.GetCustomerRepository();
            var customerBLL = new CustomerService(repo);

            var stateRepo = Factory.Current.GetCustomerStateRepository();
            var stateBLL = new CustomerStateService(stateRepo);

            _customerSLService = new CustomerSLService(customerBLL, stateBLL);

            // Crear repositorio
            var customerStateRepo = Factory.Current.GetCustomerStateRepository();

            // Crear BLL
            var customerStateBLL = new CustomerStateService(customerStateRepo);

            // Crear SL
            _customerStateSLService = new CustomerStateSLService(customerStateBLL);

            // Cargar ComboBox
            LoadStates();


        }

        /// <summary>
        /// Carga los estados de cliente en el ComboBox utilizando la capa SL.
        /// </summary>
        private void LoadStates()
        {
            try
            {
                var states = _customerStateSLService.GetAll();

                cmbState.DataSource = states;
                cmbState.DisplayMember = "Description";
                cmbState.ValueMember = "IdCustomerState";
                cmbState.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al cargar estados de cliente: {ex.Message}",
                    System.Diagnostics.Tracing.EventLevel.Error);

                MessageBox.Show("Error al cargar los estados de cliente.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// removiendo cualquier control existente.
        /// </summary>
        /// <param name="formchild">Instancia del formulario a mostrar.</param>
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
        /// Oculta columnas técnicas del grid que el usuario no debe ver.
        /// </summary>
        private void HideTechnicalColumns()
        {
            if (dataGridViewCustomers.Columns.Contains("IdCustomer"))
                dataGridViewCustomers.Columns["IdCustomer"].Visible = false;
            if (dataGridViewCustomers.Columns.Contains("State"))
                dataGridViewCustomers.Columns["State"].Visible = false;
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// usando el mismo sistema de idiomas del resto de la aplicación.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewCustomers.Columns)
            {
                // Saltamos columnas técnicas
                if (col.Name == "IdCustomer")
                    continue;

                try
                {
                    // Usamos el Name de la columna como clave en los archivos de idioma
                    // (NroDocument, FirstName, LastName, State, Telephone, Mail, Address, etc.)
                    col.HeaderText = LanguageBLL.Current.Traductor(col.Name);
                }
                catch
                {
                    // Si no se encuentra la palabra, dejamos el HeaderText tal como está
                }
            }
        }

        /// <summary>
        /// Maneja el click del botón "Registrar Cliente".
        /// Abre el formulario de registro de cliente dentro del panel contenedor.
        /// </summary>
        private void btnRegCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFormChild(new MenuRegCustomer(_panelContenedor));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir el formulario de registro de cliente: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el formulario de registro de cliente. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el click del botón "Modificar Cliente".
        /// Valida la selección en el grid y abre el formulario de modificación
        /// pasando los datos del cliente seleccionado.
        /// </summary>
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
                string comment = selectedRow.Cells["Comment"].Value?.ToString();
                string telephone = selectedRow.Cells["Telephone"].Value.ToString();
                string mail = selectedRow.Cells["Mail"].Value.ToString();
                string address = selectedRow.Cells["Address"].Value.ToString();
                string state = selectedRow.Cells["State"].Value.ToString();

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModCustomer(_panelContenedor, idCustomer, nroDocument, firstName, lastName, comment, telephone, mail, address, state));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al intentar modificar el cliente: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al intentar modificar el cliente. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el click del botón "Registrar Reserva".
        /// Valida la selección del cliente en el grid y abre el formulario
        /// de registro de reserva vinculado al cliente seleccionado.
        /// </summary>
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

                // Extraer el IdCustomer y nro de documento
                Guid idCustomer = (Guid)selectedRow.Cells["IdCustomer"].Value;
                string nroDocument = selectedRow.Cells["NroDocument"].Value.ToString();

                OpenFormChild(new MenuRegBooking(_panelContenedor, idCustomer, nroDocument));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al intentar registrar la reserva: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al intentar registrar la reserva. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el click del botón "Consultar Cliente".
        /// Toma los filtros ingresados, valida el documento y consulta
        /// a la capa de servicios para obtener el listado de clientes.
        /// </summary>
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
            int state = cmbState.SelectedIndex == -1 ? 0 : (int)cmbState.SelectedValue;

            try
            {
                var customers = _customerSLService.GetAll(nroDocumento, firstName, lastName, telephone, mail, state);

                dataGridViewCustomers.DataSource = customers.ToList();

                // Ocultar Id y traducir encabezados
                HideTechnicalColumns();
                TranslateGridHeaders();

                lblStatus.Text = customers.Any()
                    ? $"Se encontraron {customers.Count()} clientes."
                    : "No se encontraron clientes con esos criterios.";
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al buscar clientes: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al buscar clientes. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
