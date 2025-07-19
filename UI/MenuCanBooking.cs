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
using DAL.Contracts;
using DAL.Factory;
using Domain;
using UI.Helpers;

namespace UI
{
    public partial class MenuCanBooking : Form
    {


        IGenericRepository<Booking> repositoryBooking = Factory.Current.GetBookingRepository();
        private Panel _panelContenedor;
        public MenuCanBooking(Panel panelContenedor, Guid idBooking, Guid idCustomer, int nroDocument,
                      DateTime registrationBooking, TimeSpan startTime, TimeSpan endTime,
                      string field, string promotion, int state, decimal importeBooking)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization

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

        private void btnModBooking_Click(object sender, EventArgs e)
        {
            //OpenFormChild(new MenuModBooking(_panelContenedor));
        }

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        private void btnRegPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPay(_panelContenedor));
        }

        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            try
            {
                Booking canBooking = new Booking
                {
                    IdBooking = Guid.Parse(txtIdBooking.Text),
                    IdCustomer = Guid.Parse(txtIdCustomer.Text),
                    NroDocument = txtNroDocument.Text,
                    RegistrationDate = DateTime.Today,
                    RegistrationBooking = DateTime.Parse(txtRegistrationBooking.Text),
                    StartTime = TimeSpan.Parse(txtStartTime.Text),
                    EndTime = TimeSpan.Parse(txtEndTime.Text),
                    Field = Guid.Parse(txtField.Text),
                    Promotion = Guid.Parse(txtPromotion.Text),
                    State = 3
                };

                repositoryBooking.Update(canBooking.IdBooking, canBooking);

                MessageBox.Show("Reserva cancelada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            private void btnRegBooking_Click(object sender, EventArgs e)
        { 

        }
    }
}
