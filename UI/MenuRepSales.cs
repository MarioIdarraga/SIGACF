using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using SL.BLL;
using SL.Services;
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
    public partial class MenuRepSales : Form
    {
        private Panel _panelContenedor;
        private UIPayService _uiPayService;

        public MenuRepSales(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization

            IPayRepository repo = Factory.Current.GetPayRepository();
            var payService = new PayService(repo);
            _uiPayService = new UIPayService(payService); // inicialización correcta
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

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
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

        private void btnGenRepSales_Click(object sender, EventArgs e)
        {
            DateTime? since = dtpDateSinceSales.Checked ? dtpDateSinceSales.Value : (DateTime?)null;
            DateTime? until = dtpDateUntilSales.Checked ? dtpDateUntilSales.Value : (DateTime?)null;

            try
            {
                var results = _uiPayService.GetFilteredPayments(since, until);
                dataGridView1.DataSource = results.ToList();
                TranslateGridHeaders();

                lblStatus.Text = results.Any()
                    ? $"Se encontraron {results.Count()} pagos."
                    : "No se encontraron pagos con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar el reporte: " + ex.Message);
            }
        }

        private void MenuRepSales_Load(object sender, EventArgs e)
        {

        }
    }
}
