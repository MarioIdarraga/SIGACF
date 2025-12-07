using System;
using System.Windows.Forms;
using BLL.Service;
using DAL;
using Domain;
using SL;

namespace UI
{
    public partial class MenuRegPay : Form
    {
        private readonly Panel _panelContenedor;
        private readonly Guid _idBooking;
        private readonly BookingSLService _bookingSLService;

        /// <summary>
        /// Inicializa el formulario para registrar un pago.
        /// </summary>
        public MenuRegPay(
            Panel panelContenedor,
            Guid idBooking,
            int nroDocument,
            string cancha,
            string fechaReserva,
            string estado,
            decimal importe)
        {
            InitializeComponent();

            _panelContenedor = panelContenedor;

            var bookingRepo = DAL.Factory.Factory.Current.GetBookingRepository();
            var bookingService = new BookingService(bookingRepo);
            _bookingSLService = new BookingSLService(bookingService);

            _idBooking = idBooking;
            txtNroDocument.Text = nroDocument.ToString();
            txtField.Text = cancha;
            txtDateTime.Text = fechaReserva;
            txtState.Text = estado;
            txtAmount.Text = importe.ToString("N2");

            CargarMetodosDePago();
        }

        /// <summary>
        /// Carga los métodos de pago disponibles en el combo.
        /// </summary>
        private void CargarMetodosDePago()
        {
            try
            {
                var paymentMethodService = new PaymentMethodSLService();
                var list = paymentMethodService.GetAll();

                cmbPaymentMethod.DataSource = null;
                cmbPaymentMethod.DisplayMember = "Description";
                cmbPaymentMethod.ValueMember = "IdPayMethod";
                cmbPaymentMethod.DataSource = list;
                cmbPaymentMethod.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar métodos de pago: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Registra el pago y actualiza el estado de la reserva.
        /// </summary>
        private void btnRegPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPaymentMethod.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Seleccione un método de pago.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text, out decimal amount))
                {
                    MessageBox.Show(
                        "Ingrese un importe válido.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var pay = new Pay
                {
                    IdPay = 0, 
                    IdBooking = _idBooking,
                    NroDocument = int.Parse(txtNroDocument.Text),
                    Date = DateTime.Now,
                    MethodPay = (int)cmbPaymentMethod.SelectedValue,
                    Amount = amount,
                    State = 1
                };

                var repo = DAL.Factory.Factory.Current.GetPayRepository();
                var bll = new PayService(repo);
                var payMethodSL = new PaymentMethodSLService();
                var sl = new PaySLService(bll, payMethodSL);

                sl.Insert(pay);

                _bookingSLService.UpdateState(_idBooking, 2);

                MessageBox.Show(
                    "Pago registrado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar el pago: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
