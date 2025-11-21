using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Tracing;
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using UI.Helpers;
using BLL.BusinessException;
using SL.Service;

namespace UI
{
    /// <summary>
    /// Formulario para cancelar una reserva específica.
    /// Muestra los datos de la reserva seleccionada y permite
    /// marcarla como cancelada (State = 3).
    /// </summary>
    public partial class MenuCanBooking : Form
    {
        private readonly BookingSLService _bookingSLService;
        private Panel _panelContenedor;

        /// <summary>
        /// Constructor principal.
        /// Recibe el panel contenedor y los datos de la reserva a cancelar.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se incrusta el formulario.</param>
        /// <param name="idBooking">Identificador de la reserva.</param>
        /// <param name="idCustomer">Identificador del cliente.</param>
        /// <param name="nroDocument">Número de documento del cliente.</param>
        /// <param name="registrationBooking">Fecha de la reserva.</param>
        /// <param name="startTime">Hora de inicio de la reserva.</param>
        /// <param name="endTime">Hora de fin de la reserva.</param>
        /// <param name="field">Identificador de la cancha.</param>
        /// <param name="promotion">Identificador de la promoción.</param>
        /// <param name="state">Estado actual de la reserva.</param>
        /// <param name="importeBooking">Importe de la reserva.</param>
        public MenuCanBooking(
            Panel panelContenedor,
            Guid idBooking,
            Guid idCustomer,
            int nroDocument,
            DateTime registrationBooking,
            TimeSpan startTime,
            TimeSpan endTime,
            string field,
            string promotion,
            int state,
            decimal importeBooking)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Localización

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            // Asignar valores a los controles del formulario
            txtIdBooking.Text = idBooking.ToString();
            txtIdCustomer.Text = idCustomer.ToString();
            txtNroDocument.Text = nroDocument.ToString();
            txtRegistrationBooking.Text = registrationBooking.ToString();
            txtStartTime.Text = startTime.ToString();
            txtEndTime.Text = endTime.ToString();
            txtField.Text = field;
            txtPromotion.Text = promotion;
            txtState.Text = state.ToString();

            // Ocultar IDs al usuario (se usan solo internamente)
            txtIdBooking.Visible = false;
            txtIdCustomer.Visible = false;
            label10.Visible = false; // "Id reserva"
            label3.Visible = false;  // "Id Cliente"
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
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
        /// (Reservado para futura funcionalidad) Modificar la reserva.
        /// </summary>
        private void btnModBooking_Click(object sender, EventArgs e)
        {
            //OpenFormChild(new MenuModBooking(_panelContenedor));
        }

        /// <summary>
        /// Vuelve a la pantalla de búsqueda de reservas.
        /// </summary>
        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        /// <summary>
        /// Navega a la pantalla de registrar pago.
        /// </summary>
        private void btnRegPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPay(_panelContenedor));
        }

        /// <summary>
        /// Marca la reserva como cancelada (State = 3) utilizando BookingSLService.
        /// </summary>
        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Guid.TryParse(txtIdBooking.Text, out Guid idBooking))
                    throw new BusinessException("No se encontró el identificador de la reserva.");

                if (!Guid.TryParse(txtIdCustomer.Text, out Guid idCustomer))
                    throw new BusinessException("No se encontró el identificador del cliente.");

                if (!int.TryParse(txtNroDocument.Text, out int nroDoc))
                    throw new BusinessException("El número de documento no es válido.");

                if (!DateTime.TryParse(txtRegistrationBooking.Text, out DateTime registrationBooking))
                    throw new BusinessException("La fecha de reserva no es válida.");

                if (!TimeSpan.TryParse(txtStartTime.Text, out TimeSpan startTime))
                    throw new BusinessException("La hora de inicio no es válida.");

                if (!TimeSpan.TryParse(txtEndTime.Text, out TimeSpan endTime))
                    throw new BusinessException("La hora de fin no es válida.");

                if (!Guid.TryParse(txtField.Text, out Guid idField))
                    throw new BusinessException("El identificador de la cancha no es válido.");

                if (!Guid.TryParse(txtPromotion.Text, out Guid idPromotion))
                    throw new BusinessException("El identificador de la promoción no es válido.");

                // Crear objeto Booking con estado de cancelación
                Booking canBooking = new Booking
                {
                    IdBooking = idBooking,
                    IdCustomer = idCustomer,
                    NroDocument = nroDoc.ToString(),
                    RegistrationDate = DateTime.Today,
                    RegistrationBooking = registrationBooking,
                    StartTime = startTime,
                    EndTime = endTime,
                    Field = idField,
                    Promotion = idPromotion,
                    State = 3
                };

                _bookingSLService.Update(canBooking.IdBooking, canBooking);

                MessageBox.Show(
                    "Reserva cancelada con éxito.",
                    "Éxito",
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
                    $"Error inesperado al cancelar la reserva: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al cancelar la reserva. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
