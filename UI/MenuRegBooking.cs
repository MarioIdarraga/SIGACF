using BLL.BusinessException;
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using SL.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para registrar reservas.
    /// Permite seleccionar cliente, cancha, promoción, horarios
    /// y calcular el importe correspondiente.
    /// </summary>
    public partial class MenuRegBooking : Form
    {
        private Panel _panelContenedor;

        private readonly IGenericRepository<Promotion> repositoryPromotion =
            Factory.Current.GetPromotionRepository();

        private readonly IGenericRepository<Field> repositoryField =
            Factory.Current.GetFieldRepository();

        private readonly BookingSLService _bookingSLService;

        // Id del cliente asociado a la reserva
        private Guid _idCustomer;

        /// <summary>
        /// Constructor que recibe el panel contenedor y los datos del cliente.
        /// Se usa cuando se llega desde la pantalla de búsqueda de clientes.
        /// </summary>
        public MenuRegBooking(Panel panelContenedor, Guid idCustomer, string nroDocument)
        {
            InitializeComponent();

            _panelContenedor = panelContenedor;
            _idCustomer = idCustomer;

            // El Id del cliente se guarda pero no se muestra al usuario
            txtIdCustomer.Text = _idCustomer.ToString();
            txtIdCustomer.ReadOnly = true;
            txtIdCustomer.Visible = false;
            label3.Visible = false; // "Id Cliente"

            // Hora inicio/fin sólo visibles, se setean por código
            ConfigurarControlesHora();
            txtImporteBooking.ReadOnly = true;

            txtNroDocument.Text = nroDocument;

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            CargarCombos();
            dtpRegistrationBooking.Value = DateTime.Today;

            // Recalcular importe cuando cambian parámetros
            cmbField.SelectedIndexChanged += (s, e) => ActualizarImporte();
            dtpStartTime.ValueChanged += (s, e) => ActualizarImporte();
            dtpEndTime.ValueChanged += (s, e) => ActualizarImporte();
            cmbPromotion.SelectedIndexChanged += (s, e) => ActualizarImporte();
        }

        /// <summary>
        /// Constructor que solo recibe el panel contenedor.
        /// Se podría usar si aún no se seleccionó cliente.
        /// </summary>
        public MenuRegBooking(Panel panelContenedor)
        {
            InitializeComponent();
            this.Translate();

            _panelContenedor = panelContenedor;

            // Ocultar controles técnicos de Id de cliente
            txtIdCustomer.ReadOnly = true;
            txtIdCustomer.Visible = false;
            label3.Visible = false;

            // Hora inicio/fin sólo visibles, se setean por código
            ConfigurarControlesHora();
            txtImporteBooking.ReadOnly = true;

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            CargarCombos();
            dtpRegistrationBooking.Value = DateTime.Today;

            cmbField.SelectedIndexChanged += (s, e) => ActualizarImporte();
            dtpStartTime.ValueChanged += (s, e) => ActualizarImporte();
            dtpEndTime.ValueChanged += (s, e) => ActualizarImporte();
            cmbPromotion.SelectedIndexChanged += (s, e) => ActualizarImporte();
        }

        /// <summary>
        /// Configura los controles de hora para que sean de sólo lectura
        /// y muestren solo HH:mm.
        /// </summary>
        private void ConfigurarControlesHora()
        {
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "HH:mm";
            dtpStartTime.ShowUpDown = true;

            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.CustomFormat = "HH:mm";
            dtpEndTime.ShowUpDown = true;

            // Forzar minutos y segundos en 00 siempre
            dtpStartTime.ValueChanged += (s, e) =>
            {
                var fecha = dtpStartTime.Value.Date;
                var hora = dtpStartTime.Value.Hour;

                dtpStartTime.Value = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, 0, 0);
            };

            dtpEndTime.ValueChanged += (s, e) =>
            {
                var fecha = dtpEndTime.Value.Date;
                var hora = dtpEndTime.Value.Hour;

                dtpEndTime.Value = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora, 0, 0);
            };
        }

        /// <summary>
        /// Carga los combos de promociones y canchas desde la base de datos.
        /// </summary>
        private void CargarCombos()
        {
            try
            {
                // Promociones
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
                    cmbPromotion.Items.Add("No hay promociones disponibles");
                    cmbPromotion.SelectedIndex = 0;
                }

                // Canchas
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
                    cmbField.Items.Add("No hay canchas disponibles");
                    cmbField.SelectedIndex = 0;
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
                    $"Error inesperado al cargar listas de canchas/promociones: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al cargar los datos. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
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
                    "Ocurrió un error inesperado al abrir el formulario. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        //private void btnRegPay_Click(object sender, EventArgs e)
        //{
        //    OpenFormChild(new MenuRegPay(_panelContenedor));
        //}

        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindCustomers(_panelContenedor));
        }

        /// <summary>
        /// Registra la reserva con los datos ingresados.
        /// </summary>
        private void btnRegBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idCustomer == Guid.Empty && string.IsNullOrWhiteSpace(txtIdCustomer.Text))
                    throw new BusinessException("No se encontró el cliente asociado a la reserva.");

                if (string.IsNullOrWhiteSpace(txtNroDocument.Text))
                    throw new BusinessException("Debe ingresar el número de documento.");

                if (!(cmbField.SelectedItem is Field selectedField))
                    throw new BusinessException("Debe seleccionar una cancha.");

                Guid idPromotion = Guid.Empty;
                if (cmbPromotion.SelectedItem is Promotion selectedPromotion)
                    idPromotion = selectedPromotion.IdPromotion;

                if (string.IsNullOrWhiteSpace(txtImporteBooking.Text) ||
                    !decimal.TryParse(txtImporteBooking.Text, out decimal importe))
                    throw new BusinessException("El importe de la reserva no es válido.");

                Guid idCustomer =
                    _idCustomer != Guid.Empty
                        ? _idCustomer
                        : Guid.Parse(txtIdCustomer.Text);

                Booking newBooking = new Booking
                {
                    IdCustomer = idCustomer,
                    NroDocument = txtNroDocument.Text.Trim(),
                    RegistrationDate = DateTime.Today,
                    RegistrationBooking = dtpRegistrationBooking.Value,
                    StartTime = dtpStartTime.Value.TimeOfDay,
                    EndTime = dtpEndTime.Value.TimeOfDay,
                    Field = selectedField.IdField,
                    Promotion = idPromotion,
                    ImporteBooking = importe,
                    State = 1
                };

                _bookingSLService.Insert(newBooking);

                MessageBox.Show(
                    "La reserva se ha registrado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
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
                    $"Error inesperado al registrar la reserva: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al registrar la reserva. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia los campos de la pantalla de registro de reserva.
        /// </summary>
        private void LimpiarCampos()
        {
            txtNroDocument.Clear();
            dtpRegistrationBooking.Value = DateTime.Today;
            dtpStartTime.Value = DateTime.Today;
            dtpEndTime.Value = DateTime.Today;
            cmbField.SelectedIndex = -1;
            cmbPromotion.SelectedIndex = -1;
            txtImporteBooking.Text = "0.00";
            cmbHorariosDisponibles.DataSource = null;
            numericUpDown1.Value = 1;
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
        /// Obtiene y muestra los horarios disponibles para
        /// la cancha y fecha seleccionadas, según la duración elegida.
        /// </summary>
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
        /// Cuando el usuario selecciona un horario disponible,
        /// se actualizan automáticamente Hora Inicio y Hora Fin.
        /// </summary>
        private void cmbHorariosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
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

        private void CmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarImporte();
        }
    }
}

