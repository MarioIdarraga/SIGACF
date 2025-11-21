using BLL.BusinessException;
using BLL.Service;
using DAL.Factory;
using Domain;
using SL;
using SL.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario de búsqueda de reservas canceladas.
    /// Permite filtrar cancelaciones por datos del cliente
    /// y navegar hacia la pantalla de reservas.
    /// </summary>
    public partial class MenuCan : Form
    {
        private readonly BookingSLService _bookingSLService;
        private readonly BookingStateSLService _bookingStateSLService;
        private readonly Panel _panelContenedor;

        /// <summary>
        /// Constructor principal del formulario de cancelaciones.
        /// Inicializa el panel contenedor, traduce el formulario
        /// e instancia el servicio de reservas de la capa SL.
        /// </summary>
        /// <param name="panelContenedor">
        /// Panel principal donde se incrusta el formulario.
        /// </param>
        public MenuCan(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Localización

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            var stateRepo = Factory.Current.GetBookingStateRepository();
            var stateBll = new BookingStateService(stateRepo);
            _bookingStateSLService = new BookingStateSLService(stateBll);
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor
        /// removiendo cualquier control previo.
        /// </summary>
        /// <param name="formchild">Formulario hijo a mostrar.</param>
        private void OpenFormChild(object formchild)
        {
            try
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
                    $"Error inesperado al abrir formulario hijo: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el formulario. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta las columnas técnicas (IDs) del grid de cancelaciones
        /// para que el usuario no vea esos identificadores internos.
        /// </summary>
        private void HideTechnicalColumns()
        {
            if (dataGridView1.Columns.Contains("IdBooking"))
                dataGridView1.Columns["IdBooking"].Visible = false;

            if (dataGridView1.Columns.Contains("IdCustomer"))
                dataGridView1.Columns["IdCustomer"].Visible = false;

            if (dataGridView1.Columns.Contains("DVH"))
                dataGridView1.Columns["DVH"].Visible = false;
        }

        /// <summary>
        /// (Reservado para futura funcionalidad).
        /// Botón para ir a la pantalla de cancelar reserva específica.
        /// </summary>
        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            // Cuando tengas lista la pantalla de cancelación puntual,
            // descomentá esta línea:
            // OpenFormChild(new MenuCanBooking(_panelContenedor));
        }

        /// <summary>
        /// Navega a la pantalla de búsqueda de reservas.
        /// </summary>
        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        /// <summary>
        /// Ejecuta la búsqueda de reservas canceladas con los filtros ingresados
        /// y muestra el resultado en el DataGridView.
        /// </summary>
        private void btnFindCan_Click(object sender, EventArgs e)
        {
            int? nroDocumento = null;
            if (!string.IsNullOrWhiteSpace(txtNroDocument.Text))
            {
                if (int.TryParse(txtNroDocument.Text, out int result))
                    nroDocumento = result;
                else
                {
                    MessageBox.Show(
                        "Ingrese un número de documento válido.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            string firstName = string.IsNullOrWhiteSpace(txtFirstName.Text)
                ? null
                : txtFirstName.Text.Trim();

            string lastName = string.IsNullOrWhiteSpace(txtLastName.Text)
                ? null
                : txtLastName.Text.Trim();

            string telephone = string.IsNullOrWhiteSpace(txtTelephone.Text)
                ? null
                : txtTelephone.Text.Trim();

            string mail = string.IsNullOrWhiteSpace(txtMail.Text)
                ? null
                : txtMail.Text.Trim();

            try
            {
                // 1) Obtener reservas canceladas desde la SL
                var bookings = _bookingSLService.GetCanceledBookings(
                    nroDocumento,
                    firstName,
                    lastName,
                    telephone,
                    mail).ToList();

                // 2) Obtener lookup de estados Id -> Descripción
                var statesLookup = _bookingStateSLService.GetStatesLookup();

                // 3) Crear una lista con el campo "State" formateado
                var viewData = bookings.Select(b => new
                {
                    b.IdBooking,
                    b.IdCustomer,
                    b.NroDocument,
                    b.RegistrationDate,
                    b.RegistrationBooking,
                    b.StartTime,
                    b.EndTime,
                    b.Field,
                    b.Promotion,

                    // Aquí concatenamos número + descripción
                    State = statesLookup.TryGetValue(b.State, out var desc)
                        ? $"{b.State} - {desc}"
                        : b.State.ToString(),

                    b.ImporteBooking,
                    b.DVH
                }).ToList();

                // 4) Setear DataSource con el modelo preparado
                dataGridView1.DataSource = viewData;

                // 5) Ocultar columnas técnicas como siempre
                HideTechnicalColumns();

                // 6) Mensaje en la UI
                MessageBox.Show(
                    lblStatus.Text = bookings.Any()
                        ? $"Se encontraron {bookings.Count()} reservas canceladas."
                        : "No se encontraron reservas canceladas con esos criterios.",
                    "Resultado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
                    $"Error inesperado al buscar cancelaciones: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al buscar cancelaciones. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

