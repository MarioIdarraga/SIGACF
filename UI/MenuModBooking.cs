using BLL.BusinessException;
using BLL.Service;
using DAL.Contracts;
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
    public partial class MenuModBooking : Form
    {
        private Panel _panelContenedor;

        private readonly BookingSLService _bookingSLService;
        IGenericRepository<Promotion> repositoryPromotion = Factory.Current.GetPromotionRepository();
        IGenericRepository<Field> repositoryField = Factory.Current.GetFieldRepository();


        public MenuModBooking(Panel panelContenedor, Guid idBooking, Guid idCustomer, int nroDocument,
                      DateTime registrationBooking, TimeSpan startTime, TimeSpan endTime,
                      string field, string promotion, int state, decimal importeBooking, string dvh)
        {
            {
                InitializeComponent();
                dtpRegistrationBooking.Format = DateTimePickerFormat.Custom;
                dtpRegistrationBooking.CustomFormat = "dd/MM/yyyy";
                _panelContenedor = panelContenedor;
                this.Translate();

                var repo = Factory.Current.GetBookingRepository();
                var bllService = new BookingService(repo);
                _bookingSLService = new BookingSLService(bllService);

                // Asignar valores a los controles del formulario

                txtIdBooking.Text = idBooking.ToString();
                txtIdCustomer.Text = idCustomer.ToString();
                txtNroDocument.Text = nroDocument.ToString();
                dtpRegistrationBooking.Value = registrationBooking;
                dtpStartTime.Value = DateTime.Today.Add(startTime);
                dtpEndTime.Value = DateTime.Today.Add(endTime);
                cmbField.Text = field;
                cmbPromotion.Text = promotion;
                txtState.Text = state.ToString();
                txtImporteBooking.Text = importeBooking.ToString();

                CargarCombos();

                cmbHorariosDisponibles.SelectedIndexChanged += CmbHorariosDisponibles_SelectedIndexChanged;

                txtIdBooking.Visible = false;
                label9.Visible = false;

                txtIdCustomer.Visible = false;
                label3.Visible = false;

            }
        }

        /// <summary>
        /// Cuando el usuario selecciona un horario disponible,
        /// se actualizan automáticamente Hora Inicio y Hora Fin.
        /// </summary>
        private void CmbHorariosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbHorariosDisponibles.SelectedItem == null)
                    return;

                if (!TimeSpan.TryParse(cmbHorariosDisponibles.SelectedItem.ToString(), out var horaInicio))
                    return;

                int horasSeleccionadas = (int)numericUpDown1.Value;
                var duracionTurno = TimeSpan.FromHours(horasSeleccionadas);
                var horaFin = horaInicio + duracionTurno;

                var fecha = dtpRegistrationBooking.Value.Date;

                dtpStartTime.Value = fecha.Add(horaInicio).AddSeconds(-fecha.Add(horaInicio).Second);
                dtpEndTime.Value = fecha.Add(horaFin).AddSeconds(-fecha.Add(horaFin).Second);

                ActualizarImporte();
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al seleccionar horario disponible: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error al seleccionar el horario. Intente nuevamente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar promociones
                List<Promotion> promociones = repositoryPromotion.GetAll().ToList();
                if (promociones.Any())
                {
                    cmbPromotion.DataSource = promociones;
                    cmbPromotion.DisplayMember = "Name";
                    cmbPromotion.ValueMember = "IdPromotion";
                    cmbPromotion.SelectedIndex = -1;
                }
                else
                {
                    cmbPromotion.DataSource = null;
                    cmbPromotion.Items.Clear(); 
                    MessageBox.Show("No hay promociones disponibles.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPromotion.Enabled = false; 
                }

                // Cargar canchas
                List<Field> canchas = repositoryField.GetAll().ToList();
                if (canchas.Any())
                {
                    cmbField.DataSource = canchas;
                    cmbField.DisplayMember = "Name";
                    cmbField.ValueMember = "IdField";
                    cmbField.SelectedIndex = -1;
                }
                else
                {
                    cmbField.DataSource = null;
                    cmbField.Items.Clear();
                    MessageBox.Show("No hay canchas disponibles.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbField.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        //private void btnRegPay_Click(object sender, EventArgs e)
        //{
        //    OpenFormChild(new MenuRegPay(_panelContenedor));
        //}

        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            //OpenFormChild(new MenuCanBooking(_panelContenedor));
        }

        private void btnModBooking_Click(object sender, EventArgs e)
        {
            try
            {
                Booking updatedBooking = new Booking
                {
                    IdBooking = Guid.Parse(txtIdBooking.Text), 
                    IdCustomer = Guid.Parse(txtIdCustomer.Text),
                    NroDocument = txtNroDocument.Text,
                    RegistrationDate = DateTime.Today,
                    RegistrationBooking = dtpRegistrationBooking.Value,
                    StartTime = dtpStartTime.Value.TimeOfDay,
                    EndTime = dtpEndTime.Value.TimeOfDay,
                    Field = (Guid)cmbField.SelectedValue,
                    Promotion = (Guid)cmbPromotion.SelectedValue,
                    State = int.Parse(txtState.Text),
                    ImporteBooking = decimal.Parse(txtImporteBooking.Text),
                };

                _bookingSLService.Update(updatedBooking.IdBooking, updatedBooking);

                MessageBox.Show("La Reserva se ha modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnVerHorarios_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cmbField.SelectedItem is Field selectedField))
                {
                    MessageBox.Show("Debe seleccionar una cancha.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var fecha = dtpRegistrationBooking.Value.Date;

                var reservas = _bookingSLService.GetBookingsByFieldAndDate(
                    selectedField.IdField,
                    fecha);

                int horasSeleccionadas = (int)numericUpDown1.Value;
                var duracionTurno = TimeSpan.FromHours(horasSeleccionadas);

                var horariosLibres = CalcularHorariosDisponibles(reservas, duracionTurno);

                if (horariosLibres.Count == 0)
                {
                    MessageBox.Show("No hay horarios disponibles para la fecha y duración seleccionadas.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbHorariosDisponibles.DataSource = null;
                    return;
                }

                cmbHorariosDisponibles.DataSource = horariosLibres
                    .Select(h => h.ToString(@"hh\:mm"))
                    .ToList();
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
                    $"Error inesperado al obtener horarios disponibles: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al obtener los horarios disponibles. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calcula los horarios disponibles para una cancha y fecha
        /// en base a las reservas existentes.
        /// </summary>
        private List<TimeSpan> CalcularHorariosDisponibles(List<Booking> bookings, TimeSpan slotDuration)
        {
            var horariosDisponibles = new List<TimeSpan>();

            var horaApertura = new TimeSpan(8, 0, 0);
            var horaCierre = new TimeSpan(23, 0, 0);

            var reservasOrdenadas = bookings
                .OrderBy(b => b.StartTime)
                .ToList();

            for (var hora = horaApertura; hora + slotDuration <= horaCierre; hora += slotDuration)
            {
                var horaFin = hora + slotDuration;

                bool solapa = reservasOrdenadas.Any(b =>
                    !(horaFin <= b.StartTime || hora >= b.EndTime));

                if (!solapa)
                {
                    horariosDisponibles.Add(hora);
                }
            }

            return horariosDisponibles;
        }

        /// <summary>
        /// Recalcula el importe de la reserva cuando cambian
        /// cancha, horario o promoción.
        /// </summary>
        private void ActualizarImporte()
        {
            try
            {
                if (cmbField.SelectedItem is Field selectedField &&
                    dtpEndTime.Value > dtpStartTime.Value)
                {
                    TimeSpan horaInicio = dtpStartTime.Value.TimeOfDay;
                    TimeSpan horaFin = dtpEndTime.Value.TimeOfDay;

                    Guid idPromotion = Guid.Empty;
                    if (cmbPromotion.SelectedItem is Promotion selectedPromotion)
                        idPromotion = selectedPromotion.IdPromotion;

                    decimal importe = _bookingSLService.CalcularImporteReserva(
                        selectedField.IdField,
                        horaInicio,
                        horaFin,
                        idPromotion);

                    txtImporteBooking.Text = importe.ToString("0.00");
                }
                else
                {
                    txtImporteBooking.Text = "0.00";
                }
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
                    $"Error inesperado al calcular el importe de la reserva: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al calcular el importe. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void CmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarImporte();
        }
    }
}
