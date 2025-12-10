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
    /// Formulario principal del módulo de Reportes.
    /// Permite navegar hacia los reportes de ventas, cancelaciones, reservas y promociones.
    /// </summary>
    public partial class MenuRep : Form
    {
        private Panel _panelContenedor;

        /// <summary>
        /// Inicializa una nueva instancia del formulario MenuRep.
        /// </summary>
        /// <param name="panelContenedor">
        /// Panel donde se cargarán los formularios hijos de reportes.
        /// </param>
        public MenuRep(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Método de traducción existente
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
        /// <param name="formchild">Instancia del formulario hijo a visualizar.</param>
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
        /// Abre el reporte de ventas mensuales.
        /// </summary>
        private void btnRepSales_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepSales(_panelContenedor));
        }

        /// <summary>
        /// Abre el reporte de cancelaciones.
        /// </summary>
        private void btnRepCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepCan(_panelContenedor));
        }

        /// <summary>
        /// Abre el reporte de reservas.
        /// </summary>
        private void btnRepBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepBooking(_panelContenedor));
        }

        /// <summary>
        /// Abre el reporte de promociones más utilizadas.
        /// </summary>
        private void btnRepPromo_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepPromotions(_panelContenedor));
        }
    }
}

