using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Contracts;
using DAL.Factory;
using Domain;

namespace UI
{
    public partial class MenuModBooking : Form
    {
        private Panel _panelContenedor;

        IGenericRepository<Booking> repositoryBooking = Factory.Current.GetBookingRepository();
        IGenericRepository<Promotion> repositoryPromotion = Factory.Current.GetPromotionRepository();
        IGenericRepository<Field> repositoryField = Factory.Current.GetFieldRepository();


        public MenuModBooking(Panel panelContenedor, Guid idBooking, Guid idCustomer, int nroDocument,
                      DateTime registrationBooking, TimeSpan startTime, TimeSpan endTime, string field,
                      string promotion, int state)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            // Asignar valores a los controles del formulario

            txtIdBooking.Text = idBooking.ToString();
            txtIdCustomer.Text = idCustomer.ToString();
            txtNroDocument.Text = nroDocument.ToString();
            dtpRegistrationBooking.Value = registrationBooking;
            dtpStartTime.Value = DateTime.Today.Add(startTime); // Convert TimeSpan to DateTime
            dtpEndTime.Value = DateTime.Today.Add(endTime); // Convert TimeSpan to DateTime
            cmbField.Text = field;
            cmbPromotion.Text = promotion;
            txtState.Text = state.ToString();

            CargarCombos(); // Cargar opciones en los combos
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
                    cmbPromotion.DisplayMember = "Name"; // Ajustar con el nombre exacto de la propiedad
                    cmbPromotion.ValueMember = "IdPromotion";
                    cmbPromotion.SelectedIndex = -1; // No seleccionar ninguno por defecto
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
                    cmbField.DisplayMember = "Name"; // Ajustar con el nombre exacto de la propiedad
                    cmbField.ValueMember = "IdField";
                    cmbField.SelectedIndex = -1;
                }
                else
                {
                    cmbField.DataSource = null;
                    cmbField.Items.Add("No hay canchas disponibles");
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
    }
}
