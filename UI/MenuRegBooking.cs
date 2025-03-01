using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using static System.Windows.Forms.AxHost;

namespace UI
{
    public partial class MenuRegBooking : Form
    {
        private Panel _panelContenedor;

        IGenericRepository<Booking> repositoryBooking = Factory.Current.GetBookingRepository();
        IGenericRepository<State> repositoryState = Factory.Current.GetStateRepository();
        IGenericRepository<Promotion> repositoryPromotion = Factory.Current.GetPromotionRepository();

        public MenuRegBooking(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            CargarCombos();
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar ComboBox de Estado desde la base de datos
                List<State> estados = repositoryState.GetAll().ToList();

                // Cargar ComboBox de Promociones desde la base de datos
                List<Promotion> promociones = repositoryPromotion.GetAll().ToList();
                cmbPromotion.DataSource = promociones;
                cmbPromotion.DisplayMember = "Descripcion";  // Ajustar según la propiedad correcta en la entidad Promotion
                cmbPromotion.ValueMember = "Id";
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
                Booking newBooking = new Booking
                {
                    IdCustomer = Guid.Parse(txtIdCustomer.Text),
                    RegistrationBooking = DateTime.Today,
                    StartTime = dtpStartTime.Value,
                    EndTime = dtpEndTime.Value,
                    Promotion = (int)cmbPromotion.SelectedValue,  // Se usa SelectedValue ya que los IDs vienen de la BD
                    State = 0
                };

                repositoryBooking.Insert(newBooking);

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
            txtIdCustomer.Text = "";
            dtpRegistrationBooking.Value = DateTime.Now;
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            cmbPromotion.SelectedIndex = -1;
        }
    }
}

