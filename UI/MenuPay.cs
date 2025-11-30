using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.Service;
using DAL.Factory;
using Domain;
using SL;
using UI.Helpers;

namespace UI
{
    public partial class MenuPay : Form
    {
        private readonly PaySLService _paySLService;
        private readonly PaymentMethodSLService _paymentMethodSLService;
        private readonly Panel _panelContenedor;

        /// <summary>
        /// Inicializa la pantalla de búsqueda de pagos.
        /// </summary>
        public MenuPay(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var repo = Factory.Current.GetPayRepository();
            var bll = new PayService(repo);
            _paySLService = new PaySLService(bll);

            _paymentMethodSLService = new PaymentMethodSLService();

            CargarMetodosDePago();
        }

        /// <summary>
        /// Carga los métodos de pago habilitados en el combo.
        /// </summary>
        private void CargarMetodosDePago()
        {
            try
            {
                var list = _paymentMethodSLService.GetAll().ToList();

                cmbMethodPayment.DataSource = list;
                cmbMethodPayment.DisplayMember = "Description";
                cmbMethodPayment.ValueMember = "IdPayMethod";
                cmbMethodPayment.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar métodos de pago: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ejecuta la búsqueda de pagos aplicando filtros de fecha y método.
        /// </summary>
        private void btnFindPay_Click(object sender, EventArgs e)
        {
            try
            {
                // Fecha desde
                DateTime? dateFrom = dtpPayFrom.Checked
                    ? dtpPayFrom.Value.Date
                    : (DateTime?)null;

                // Fecha hasta (23:59:59)
                DateTime? dateTo = dtpPayTo.Checked
                    ? dtpPayTo.Value.Date.AddDays(1).AddTicks(-1)
                    : (DateTime?)null;

                // Método
                int? method = null;
                if (cmbMethodPayment.SelectedValue != null)
                    method = Convert.ToInt32(cmbMethodPayment.SelectedValue);

                // Buscar pagos
                var list = _paySLService.GetAll(null, dateFrom, dateTo, method).ToList();

                dataGridViewPay.DataSource = list;
                FormatearGrilla();

                lblStatus.Text = list.Any()
                    ? $"Se encontraron {list.Count} pagos."
                    : "No se encontraron pagos.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar pagos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Aplica formato visual al DataGridView (nro pago, colores y ocultar columnas).
        /// </summary>
        private void FormatearGrilla()
        {

            Ocultar("IdBooking");
            Ocultar("State");

            // Colorear según estado
            foreach (DataGridViewRow row in dataGridViewPay.Rows)
            {
                int estado = Convert.ToInt32(row.Cells["State"].Value);

                switch (estado)
                {
                    case 1: row.DefaultCellStyle.BackColor = Color.LightGreen; break;
                    case 2: row.DefaultCellStyle.BackColor = Color.Salmon; break;
                }
            }
        }

        /// <summary>
        /// Oculta una columna si existe.
        /// </summary>
        private void Ocultar(string col)
        {
            if (dataGridViewPay.Columns.Contains(col))
                dataGridViewPay.Columns[col].Visible = false;
        }
    }
}


