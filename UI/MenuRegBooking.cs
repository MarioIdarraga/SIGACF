using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using static System.Windows.Forms.AxHost;

namespace UI
{
    public partial class MenuRegBooking : Form
    {
        private Panel _panelContenedor;

        IGenericRepository<Promotion> repositoryPromotion = Factory.Current.GetPromotionRepository();
        IGenericRepository<Field> repositoryField = Factory.Current.GetFieldRepository();
        private readonly BookingSLService _bookingSLService;


        private Guid _idCustomer; // Variable para almacenar el Id del cliente

        public MenuRegBooking(Panel panelContenedor, Guid idCustomer, string nroDocument)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _idCustomer = idCustomer; // Guardar el Id del cliente
            txtNroDocument.Text = nroDocument;


            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            CargarCombos();
            dtpRegistrationBooking.Value = DateTime.Today;

            // Cargar el ID del cliente en un control, por ejemplo un TextBox o Label
            txtIdCustomer.Text = _idCustomer.ToString(); // Si tienes un TextBox

            cmbField.SelectedIndexChanged += (s, e) => ActualizarImporte();
            dtpStartTime.ValueChanged += (s, e) => ActualizarImporte();
            dtpEndTime.ValueChanged += (s, e) => ActualizarImporte();
            cmbPromotion.SelectedIndexChanged += (s, e) => ActualizarImporte();

        }

        public MenuRegBooking(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            CargarCombos();
            dtpRegistrationBooking.Value = DateTime.Today;

            cmbField.SelectedIndexChanged += (s, e) => ActualizarImporte();
            dtpStartTime.ValueChanged += (s, e) => ActualizarImporte();
            dtpEndTime.ValueChanged += (s, e) => ActualizarImporte();
            cmbPromotion.SelectedIndexChanged += (s, e) => ActualizarImporte();

        }

        private void CargarCombos()
        {
            try
            {
                // Cargar ComboBox de Promociones desde la base de datos
                List<Promotion> promociones = repositoryPromotion.GetAll().ToList();
                if (promociones.Any()) // Si hay datos en la lista
                {
                    cmbPromotion.DataSource = promociones;
                    cmbPromotion.DisplayMember = "Name"; 
                    cmbPromotion.ValueMember = "IdPromotion";
                    cmbPromotion.SelectedIndex = -1; 
                }
                else
                {
                    cmbPromotion.DataSource = null;
                    cmbPromotion.Items.Add("No hay promociones disponibles");
                }

                // Cargar ComboBox de Canchas desde la base de datos
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

        private void btnRegPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPay(_panelContenedor));
        }

        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindCustomers(_panelContenedor));
        }

        private void btnRegBooking_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedField = cmbField.SelectedItem as Field;
                var selectedPromotion = cmbPromotion.SelectedItem as Promotion;

                Booking newBooking = new Booking
                {
                    IdCustomer = Guid.Parse(txtIdCustomer.Text),
                    NroDocument = txtNroDocument.Text,
                    RegistrationDate = DateTime.Today,
                    RegistrationBooking = dtpRegistrationBooking.Value,
                    StartTime = dtpStartTime.Value.TimeOfDay,
                    EndTime = dtpEndTime.Value.TimeOfDay,
                    Field = selectedField?.IdField ?? Guid.Empty,
                    Promotion = selectedPromotion?.IdPromotion ?? Guid.Empty,
                    ImporteBooking = decimal.Parse(txtImporteBooking.Text),
                    State = 0
                };

                _bookingSLService.Insert(newBooking);

                MessageBox.Show("Reserva guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LimpiarCampos()
        {
            txtIdCustomer.Clear();
            txtNroDocument.Clear();
            dtpRegistrationBooking.Value = DateTime.Now;
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            cmbField.SelectedIndex = -1;
            cmbPromotion.SelectedIndex = -1;
        }
        private void ActualizarImporte()
        {
            try
            {
                if (cmbField.SelectedItem is Field selectedField)
                {
                    TimeSpan horaInicio = dtpStartTime.Value.TimeOfDay;
                    TimeSpan horaFin = dtpEndTime.Value.TimeOfDay;

                    Guid idPromotion = Guid.Empty;
                    if (cmbPromotion.SelectedItem is Promotion selectedPromotion)
                    {
                        idPromotion = selectedPromotion.IdPromotion;
                    }

                    decimal importe = _bookingSLService.CalcularImporteReserva(selectedField.IdField, horaInicio, horaFin, idPromotion);
                    txtImporteBooking.Text = importe.ToString("0.00");
                }
                else
                {
                    txtImporteBooking.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular el importe: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void CmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarImporte();
        }
    }
}

