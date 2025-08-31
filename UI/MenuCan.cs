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
using DAL.Factory;
using SL;
using UI.Helpers;

namespace UI
{
    public partial class MenuCan : Form
    {

        private readonly BookingSLService _bookingSLService;
        private Panel _panelContenedor;

        public MenuCan(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);
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

        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            //OpenFormChild(new MenuCanBooking(_panelContenedor));
        }

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        private void btnFindCan_Click(object sender, EventArgs e)
        {
            int? nroDocumento = null;
            if (!string.IsNullOrWhiteSpace(txtNroDocument.Text) && int.TryParse(txtNroDocument.Text, out int result))
                nroDocumento = result;

            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string telephone = txtTelephone.Text.Trim();
            string mail = txtMail.Text.Trim();

            try
            {
                var bookings = _bookingSLService.GetCanceledBookings(nroDocumento, firstName, lastName, telephone, mail);

                dataGridView1.DataSource = bookings.ToList();

                MessageBox.Show(
                    bookings.Any()
                        ? $"Se encontraron {bookings.Count()} reservas canceladas."
                        : "No se encontraron reservas canceladas con esos criterios.",
                    "Resultado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cancelaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
