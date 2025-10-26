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
using Domain;
using UI.Helpers;

namespace UI
{
    public partial class MenuRegPay : Form
    {
        private Panel _panelContenedor;

        public MenuRegPay(Panel panelContenedor, Guid idBooking, int nroReserva, decimal importeBooking, string estado)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate();


            // Asignar valores a los controles del formulario
            //txtIdBooking.Text = idBooking.ToString();
            //txtNroReserva.Text = nroReserva.ToString();
            //txtImporteBooking.Text = importeBooking.ToString("F2"); // Formatear a 2 decimales
            //txtEstado.Text = estado;
            

        }

        public MenuRegPay(Panel panelContenedor)
        {
            InitializeComponent();
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

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        private void btnRegPay_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Guid idBooking = Guid.Parse(txtBookingId.Text.Trim());
            //    decimal amount = decimal.Parse(txtAmount.Text.Trim());
            //    int methodPay = cmbMethodPay.SelectedIndex; // Asegurate de que los valores estén bien cargados
            //    int state = int.Parse(txtState.Text.Trim());

            //    Pay newPay = new Pay
            //    {
            //        IdPay = Guid.NewGuid(),
            //        IdBooking = idBooking,
            //        NroDocument = 0, // Si lo querés usar, pasalo también
            //        Date = DateTime.Now,
            //        MethodPay = methodPay,
            //        Amount = amount,
            //        State = state
            //    };

            //    var payRepo = DAL.Factory.Factory.Current.GetPayRepository();
            //    var payBLL = new BLL.Service.PayService(payRepo);
            //    var paySL = new SL.Service.PaySLService(payBLL);

            //    paySL.RegisterPay(newPay); // Guarda el pago

            //    // Actualizar estado de la reserva a "Pagado"
            //    var bookingRepo = DAL.Factory.Factory.Current.GetBookingRepository();
            //    var bookingBLL = new BLL.Service.BookingService(bookingRepo);
            //    var bookingSL = new SL.Service.BookingSLService(bookingBLL);

            //    bookingSL.UpdateBookingState(idBooking, 2); // Por ejemplo: 2 = Pagado

            //    MessageBox.Show("Pago registrado y reserva actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al registrar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
