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
                      string field, string promotion, int state, decimal importeBooking)
        {
            {
                InitializeComponent();
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

                CargarCombos();
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

        private void btnRegPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPay(_panelContenedor));
        }

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
                };

                _bookingSLService.Update(updatedBooking.IdBooking, updatedBooking);

                MessageBox.Show("Reserva modificada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
