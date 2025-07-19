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
    public partial class MenuSales : Form
    {

        private readonly BookingSLService _bookingSLService;

        private Panel _panelContenedor;

        public MenuSales(Panel panelContenedor)
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

        private void btnModBooking_Click(object sender, EventArgs e)
        {

            try
            {
                // Validar si hay filas en el DataGridView
                if (dataGridViewBookings.Rows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario de la lista para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener datos de la fila seleccionada
                DataGridViewRow selectedRow = dataGridViewBookings.SelectedRows[0];

                // Validar que las celdas no sean nulas
                if (selectedRow.Cells["IdBooking"].Value == null ||
                selectedRow.Cells["IdCustomer"].Value == null ||
                selectedRow.Cells["NroDocument"].Value == null ||
                selectedRow.Cells["RegistrationBooking"].Value == null ||
                selectedRow.Cells["StartTime"].Value == null ||
                selectedRow.Cells["EndTime"].Value == null ||
                selectedRow.Cells["Field"].Value == null ||
                selectedRow.Cells["Promotion"].Value == null ||
                selectedRow.Cells["State"].Value == null)
                {
                    MessageBox.Show("La reserva seleccionada tiene datos inválidos. Intente seleccionar otra.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extraer valores de la fila
                Guid idBooking = (Guid)selectedRow.Cells["IdBooking"].Value;
                Guid idCustomer = (Guid)selectedRow.Cells["IdCustomer"].Value;
                int nroDocument = Convert.ToInt32(selectedRow.Cells["NroDocument"].Value);
                DateTime registrationBooking = Convert.ToDateTime(selectedRow.Cells["RegistrationBooking"].Value);
                TimeSpan startTime = (TimeSpan)selectedRow.Cells["StartTime"].Value; 
                TimeSpan endTime = (TimeSpan)selectedRow.Cells["EndTime"].Value;
                string field = selectedRow.Cells["Field"].Value.ToString();
                string promotion = selectedRow.Cells["Promotion"].Value.ToString();
                int state = Convert.ToInt32(selectedRow.Cells["State"].Value);
                decimal importeBooking = 0;

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModBooking(_panelContenedor, idBooking, idCustomer, nroDocument,
                                                 registrationBooking, startTime,
                                                 endTime, field, promotion, state, importeBooking));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegBooking(_panelContenedor));
        }

        private void btnRegPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBookings.Rows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una reserva de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = dataGridViewBookings.SelectedRows[0];

                if (selectedRow.Cells["IdBooking"].Value == null ||
                    selectedRow.Cells["State"].Value == null ||
                    selectedRow.Cells["Promotion"].Value == null)
                {
                    MessageBox.Show("La reserva seleccionada tiene datos inválidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid idBooking = (Guid)selectedRow.Cells["IdBooking"].Value;
                int nroReserva = selectedRow.Index + 1; 
                string estado = selectedRow.Cells["State"].Value.ToString();
                decimal importeBooking = Convert.ToDecimal(selectedRow.Cells["ImporteBooking"].Value);

                //// Calcular importe según promoción (este dato debe venir cargado)
                //string promo = selectedRow.Cells["Promotion"].Value.ToString();
                //decimal importe = CalcularImporteDesdePromocion(promo); // tu lógica

                OpenFormChild(new MenuRegPay(_panelContenedor, idBooking, nroReserva, importeBooking, estado));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si hay filas en el DataGridView
                if (dataGridViewBookings.Rows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario de la lista para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener datos de la fila seleccionada
                DataGridViewRow selectedRow = dataGridViewBookings.SelectedRows[0];

                // Validar que las celdas no sean nulas
                if (selectedRow.Cells["IdBooking"].Value == null ||
                selectedRow.Cells["IdCustomer"].Value == null ||
                selectedRow.Cells["NroDocument"].Value == null ||
                selectedRow.Cells["RegistrationBooking"].Value == null ||
                selectedRow.Cells["StartTime"].Value == null ||
                selectedRow.Cells["EndTime"].Value == null ||
                selectedRow.Cells["Field"].Value == null ||
                selectedRow.Cells["Promotion"].Value == null ||
                selectedRow.Cells["State"].Value == null)

                {
                    MessageBox.Show("La reserva seleccionada tiene datos inválidos. Intente seleccionar otra.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Extraer valores de la fila
                Guid idBooking = (Guid)selectedRow.Cells["IdBooking"].Value;
                Guid idCustomer = (Guid)selectedRow.Cells["IdCustomer"].Value;
                int nroDocument = Convert.ToInt32(selectedRow.Cells["NroDocument"].Value);
                DateTime registrationBooking = Convert.ToDateTime(selectedRow.Cells["RegistrationBooking"].Value);
                TimeSpan startTime = (TimeSpan)selectedRow.Cells["StartTime"].Value;
                TimeSpan endTime = (TimeSpan)selectedRow.Cells["EndTime"].Value;
                string field = selectedRow.Cells["Field"].Value.ToString();
                string promotion = selectedRow.Cells["Promotion"].Value.ToString();
                int state = Convert.ToInt32(selectedRow.Cells["State"].Value);
                decimal importeBooking = 0; 

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuCanBooking(_panelContenedor, idBooking, idCustomer, nroDocument,
                                                 registrationBooking, startTime,
                                                 endTime, field, promotion, state, importeBooking));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            int? nroDocumento = null;
            if (!string.IsNullOrWhiteSpace(txtNroDocument.Text))
            {
                if (int.TryParse(txtNroDocument.Text, out int result))
                {
                    nroDocumento = result;
                }
                else
                {
                    MessageBox.Show("Ingrese un número de documento válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DateTime? registrationBooking = dtpRegistrationBooking.Checked ? dtpRegistrationBooking.Value : (DateTime?)null;
            DateTime? registrationDate = dtpRegistrationDate.Checked ? dtpRegistrationDate.Value : (DateTime?)null;

            try
            {
                var bookings = _bookingSLService.GetAll(nroDocumento, registrationBooking, registrationDate);

                // Mostrar resultados en un DataGridView
                dataGridViewBookings.DataSource = bookings.ToList();


                lblStatus.Text = bookings.Any()
                    ? $"Se encontraron {bookings.Count()} reservas."
                    : "No se encontraron reservas con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

