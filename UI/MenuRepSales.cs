using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class MenuRepSales : Form
    {
        private Panel _panelContenedor;

        public MenuRepSales(Panel panelContenedor)
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

        private void btnRepBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepBooking(_panelContenedor));
        }

        private void btnRepCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepCan(_panelContenedor));
        }

        private void btnGenRepSales_Click(object sender, EventArgs e)
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

            DateTime? registrationBooking = dtpDateSinceSales.Checked ? dtpDateSinceSales.Value : (DateTime?)null;
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
