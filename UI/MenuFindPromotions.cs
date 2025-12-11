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
    /// <summary>
    /// Formulario para la búsqueda, listado y administración de promociones.
    /// Permite navegar hacia otros submenús y consultar promociones mediante la capa SL.
    /// </summary>
    public partial class MenuFindPromotions : Form
    {
        private Panel _panelContenedor;

        private readonly PromotionSLService _promotionSLService;

        /// <summary>
        /// Constructor del formulario. Inicializa servicios, aplica traducción
        /// y recibe el panel contenedor donde se cargarán los subformularios.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se incrustarán los formularios secundarios.</param>
        public MenuFindPromotions(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var repo = DAL.Factory.Factory.Current.GetPromotionRepository();
            var bll = new PromotionService(repo);
            _promotionSLService = new PromotionSLService(bll);
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// eliminando el previo si existe.
        /// </summary>
        /// <param name="formchild">Instancia de formulario a mostrar.</param>
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

        /// <summary>
        /// Abre el menú de administración general.
        /// </summary>
        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        /// <summary>
        /// Abre el formulario para registrar nuevas promociones.
        /// </summary>
        private void btnRegPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegProm(_panelContenedor));
        }

        /// <summary>
        /// Abre el formulario para modificar promociones existentes.
        /// </summary>
        private void btnModPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModProm(_panelContenedor));
        }

        /// <summary>
        /// Ejecuta la búsqueda de promociones utilizando la capa SL,
        /// muestra resultados en el DataGridView y traduce encabezados.
        /// </summary>
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

        /// <summary>
        /// Oculta columnas internas o técnicas del DataGridView.
        /// </summary>
        private void HideTechnicalColumns()
        {
            if (dataGridViewUsers.Columns.Contains("IdPromotion"))
                dataGridViewUsers.Columns["IdPromotion"].Visible = false;
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el sistema de idiomas de la aplicación.
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
                    // Si no tiene traducción, se deja como viene.
                }
            }
        }
    }
}

