using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Factory;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para registrar nuevas promociones.
    /// Permite navegar hacia el formulario de búsqueda y alojar subformularios dentro del panel contenedor.
    /// </summary>
    public partial class MenuRegProm : Form
    {
        private Panel _panelContenedor;

        /// <summary>
        /// Constructor del formulario. Inicializa componentes, aplica traducciones
        /// y obtiene el repositorio de promociones.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se cargarán formularios secundarios.</param>
        public MenuRegProm(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var promotionRepo = Factory.Current.GetPromotionRepository();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// eliminando cualquier formulario previo.
        /// </summary>
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
        /// Navega al formulario de búsqueda de promociones.
        /// </summary>
        private void btnFindPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPromotions(_panelContenedor));
        }

        /// <summary>
        /// Acción del botón Registrar Promoción.
        /// </summary>
        private void btnRegPromotion_Click(object sender, EventArgs e)
        {
        }
    }
}
