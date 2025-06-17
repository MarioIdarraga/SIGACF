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
using UI.Helpers;

namespace UI
{
    public partial class MenuSales : Form
    {

        IGenericRepository<Booking> repositoryBooking = Factory.Current.GetBookingRepository();

        private Panel _panelContenedor;

        public MenuSales(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization
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

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModBooking(_panelContenedor, idBooking, idCustomer, nroDocument,
                                                 registrationBooking, startTime,
                                                 endTime, field, promotion, state));
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
            OpenFormChild(new MenuRegPay(_panelContenedor));
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

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuCanBooking(_panelContenedor, idBooking, idCustomer, nroDocument,
                                                 registrationBooking, startTime,
                                                 endTime, field, promotion, state));
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
                // Llamada a la DAL
                var booking = repositoryBooking.GetAll(nroDocumento, registrationBooking, registrationDate);


                // Mostrar resultados en un DataGridView
                dataGridViewBookings.DataSource = booking.ToList();

                // Mensaje en la UI
                lblStatus.Text = booking.Any()
                    ? $"Se encontraron {booking.Count()} clientes."
                    : "No se encontraron clientes con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
