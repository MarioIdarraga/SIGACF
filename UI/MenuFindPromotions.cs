using BLL.Service;
using DAL.Factory;
using SL;
using SL.BLL;
using SL.Service.Extension;
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
    public partial class MenuFindPromotions : Form
    {
        private Panel _panelContenedor;

        private readonly PromotionSLService _promotionSLService;

        public MenuFindPromotions(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var repo = DAL.Factory.Factory.Current.GetPromotionRepository();
            var bll = new PromotionService(repo);
            _promotionSLService = new PromotionSLService(bll);
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
        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        private void btnRegPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegProm(_panelContenedor));
        }

        private void btnModPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModProm(_panelContenedor));
        }

        
        private void btnFindPromotion_Click(object sender, EventArgs e)
        {
            try
            {
                var list = _promotionSLService.GetAll();

                dataGridViewUsers.DataSource = list;

                lblStatus.Text = list.Any()
                    ? $"Se encontraron {list.Count} promociones."
                    : "No se encontraron promociones.";

                HideTechnicalColumns();
                TranslateGridHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar promociones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideTechnicalColumns()
        {
            if (dataGridViewUsers.Columns.Contains("IdPromotion"))
                dataGridViewUsers.Columns["IdPromotion"].Visible = false;
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewUsers.Columns)
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
    }
}
