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
        IGenericRepository<Promotion> repositoryPromotion = Factory.Current.GetPromotionRepository();
        IGenericRepository<Field> repositoryField = Factory.Current.GetFieldRepository();

        private Guid _idCustomer; // Variable para almacenar el Id del cliente

        public MenuRegBooking(Panel panelContenedor, Guid idCustomer, string nroDocument)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _idCustomer = idCustomer; // Guardar el Id del cliente
            txtNroDocument.Text = nroDocument;

            CargarCombos();
            dtpRegistrationBooking.Value = DateTime.Today;

            // Cargar el ID del cliente en un control, por ejemplo un TextBox o Label
            txtIdCustomer.Text = _idCustomer.ToString(); // Si tienes un TextBox
        }

        public MenuRegBooking(Panel panelContenedor)
        {
            _panelContenedor = panelContenedor;
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
                    NroDocument = txtNroDocument.Text,
                    RegistrationDate = DateTime.Today,
                    RegistrationBooking = dtpRegistrationBooking.Value,
                    StartTime = dtpStartTime.Value,
                    EndTime = dtpEndTime.Value,
                    Field = (Guid)cmbField.SelectedValue,
                    Promotion = (Guid)cmbPromotion.SelectedValue, 
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
            txtIdCustomer.Clear();
            txtNroDocument.Clear();
            dtpRegistrationBooking.Value = DateTime.Now;
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            cmbField.Items.Clear();
            cmbPromotion.SelectedIndex = -1;
        }
    }
}

