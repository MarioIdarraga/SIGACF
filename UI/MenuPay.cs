using BLL.BusinessException;
using BLL.Service;
using DAL.Factory;
using Domain;
using SL;
using SL.BLL;
using SL.Service;
using System;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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

            var payRepo = Factory.Current.GetPayRepository();
            var payBLL = new PayService(payRepo);

            _paymentMethodSLService = new PaymentMethodSLService();

            _paySLService = new PaySLService(payBLL, _paymentMethodSLService);

            CargarMetodosDePago();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
        private void OpenFormChild(object formchild)
        {
            try
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
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir formulario hijo: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el formulario. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewPay.Columns)
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
                TranslateGridHeaders();
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
            Ocultar("MethodPay"); 

            foreach (DataGridViewRow row in dataGridViewPay.Rows)
            {
                int estado = Convert.ToInt32(row.Cells["State"].Value);

                if (dataGridViewPay.Columns.Contains("PaymentMethodDescription"))
                    dataGridViewPay.Columns["PaymentMethodDescription"].HeaderText = "Método de Pago";

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

        private void btnRegCustomer_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegCustomer(_panelContenedor));
        }
    }
}


