using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using SL.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            this.Translate(); 

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
                    MessageBox.Show("Debe seleccionar una reserva de la lista para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                string dvh = selectedRow.Cells["DVH"].Value?.ToString() ?? string.Empty;

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModBooking(_panelContenedor, idBooking, idCustomer, nroDocument,
                                                 registrationBooking, startTime,
                                                 endTime, field, promotion, state, importeBooking, dvh));
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

        /// <summary>
        /// Abre el formulario de registro de pago con los datos de la reserva seleccionada.
        /// </summary>
        /// <summary>
        /// Abre el formulario de registro de pago usando la reserva seleccionada.
        /// </summary>
        private void btnRegPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBookings.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una reserva.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dataGridViewBookings.SelectedRows[0];

                Guid idBooking = (Guid)row.Cells["IdBooking"].Value;
                string cancha = row.Cells["Description"].Value?.ToString() ?? "";
                string estado = row.Cells["StateDescription"].Value?.ToString() ?? "";
                int nroDoc = Convert.ToInt32(row.Cells["NroDocument"].Value);
                string fechaReserva = row.Cells["RegistrationBooking"].Value.ToString();
                decimal importe = Convert.ToDecimal(row.Cells["ImporteBooking"].Value);

                OpenFormChild(new MenuRegPay(
                    _panelContenedor,
                    idBooking,
                    nroDoc,
                    cancha,
                    fechaReserva,
                    estado,
                    importe
                ));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el registro de pago: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                int state = Convert.ToInt32(selectedRow.Cells["State"].Value);
                if (state == 3)
                {
                    MessageBox.Show("La reserva ya está en estado 3 (cancelada/no vigente). No se puede volver a cancelar.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Guid idBooking = (Guid)selectedRow.Cells["IdBooking"].Value;
                Guid idCustomer = (Guid)selectedRow.Cells["IdCustomer"].Value;
                int nroDocument = Convert.ToInt32(selectedRow.Cells["NroDocument"].Value);
                DateTime registrationBooking = Convert.ToDateTime(selectedRow.Cells["RegistrationBooking"].Value);
                TimeSpan startTime = (TimeSpan)selectedRow.Cells["StartTime"].Value;
                TimeSpan endTime = (TimeSpan)selectedRow.Cells["EndTime"].Value;
                string field = selectedRow.Cells["Field"].Value.ToString();
                string promotion = selectedRow.Cells["Promotion"].Value.ToString();
                decimal importeBooking = 0;


                OpenFormChild(new MenuCanBooking(_panelContenedor, idBooking, idCustomer, nroDocument,
                                                 registrationBooking, startTime,
                                                 endTime, field, promotion, state, importeBooking));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la reserva: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewBookings.Columns)
            {
                // Saltar columnas técnicas
                if (col.Name == "UserId")
                    continue;

                try
                {
                    col.HeaderText = LanguageBLL.Current.Traductor(col.Name);
                }
                catch
                {
                    // Si no se encuentra la traducción, se deja el HeaderText tal cual.
                }
            }
        }


        /// <summary>
        /// Busca reservas según los filtros ingresados y actualiza el listado.
        /// </summary>
        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            int? nroDocumento = null;

            if (!string.IsNullOrWhiteSpace(txtNroDocument.Text))
            {
                if (int.TryParse(txtNroDocument.Text, out int result))
                    nroDocumento = result;
                else
                {
                    MessageBox.Show("Ingrese un número de documento válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DateTime? registrationBooking = dtpRegistrationBooking.Checked ? dtpRegistrationBooking.Value : (DateTime?)null;
            DateTime? registrationDate = dtpRegistrationDate.Checked ? dtpRegistrationDate.Value : (DateTime?)null;

            try
            {
                var bookings = _bookingSLService.GetAll(nroDocumento, registrationBooking, registrationDate);
                dataGridViewBookings.DataSource = bookings.ToList();

                if (dataGridViewBookings.Columns.Contains("StartTime"))
                    dataGridViewBookings.Columns["StartTime"].DefaultCellStyle.Format = @"hh\:mm";

                if (dataGridViewBookings.Columns.Contains("EndTime"))
                    dataGridViewBookings.Columns["EndTime"].DefaultCellStyle.Format = @"hh\:mm";

                if (!dataGridViewBookings.Columns.Contains("NroReserva"))
                {
                    dataGridViewBookings.Columns.Add("NroReserva", "Nro Reserva");
                    dataGridViewBookings.Columns["NroReserva"].DisplayIndex = 0;
                }

                foreach (DataGridViewRow row in dataGridViewBookings.Rows)
                {
                    Guid id = (Guid)row.Cells["IdBooking"].Value;
                    row.Cells["NroReserva"].Value = $"R-{id.ToString().Substring(0, 6).ToUpper()}";
                }

                OcultarColumna("IdBooking");
                OcultarColumna("IdCustomer");
                OcultarColumna("Field");
                OcultarColumna("Promotion");
                OcultarColumna("DVH");
                OcultarColumna("State");
                OcultarColumna("RegistrationDate");

                if (dataGridViewBookings.Columns.Contains("Description"))
                {
                    dataGridViewBookings.Columns["Description"].HeaderText = "Cancha";
                    dataGridViewBookings.Columns["Description"].DisplayIndex = 3;
                }

                if (dataGridViewBookings.Columns.Contains("StateDescription"))
                {
                    dataGridViewBookings.Columns["StateDescription"].HeaderText = "Estado";
                    dataGridViewBookings.Columns["StateDescription"].DisplayIndex = 4;
                }

                foreach (DataGridViewRow row in dataGridViewBookings.Rows)
                {
                    string estado = row.Cells["StateDescription"].Value?.ToString();

                    switch (estado)
                    {
                        case "Pendiente":
                            row.DefaultCellStyle.BackColor = Color.Khaki;
                            break;

                        case "Confirmada":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;

                        case "Cancelada":
                            row.DefaultCellStyle.BackColor = Color.Salmon;
                            break;
                    }
                }

                TranslateGridHeaders();

                lblStatus.Text = bookings.Any()
                    ? $"Se encontraron {bookings.Count()} reservas."
                    : "No se encontraron reservas con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar reservas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Oculta una columna del DataGridView si existe.
        /// </summary>
        private void OcultarColumna(string columnName)
        {
            if (dataGridViewBookings.Columns.Contains(columnName))
                dataGridViewBookings.Columns[columnName].Visible = false;
        }

    }
}

