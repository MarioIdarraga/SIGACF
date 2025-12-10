using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Factory;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para modificar promociones existentes.
    /// Permite navegar hacia búsqueda, registro y modificación de promociones.
    /// </summary>
    public partial class MenuModProm : Form
    {
        private Panel _panelContenedor;

        //private readonly PromotionSLService _promotionSLService;

        /// <summary>
        /// Constructor extendido que permite recibir múltiples datos relacionados,
        /// actualmente no utilizados pero mantenidos para compatibilidad.
        /// </summary>
        public MenuModProm(Panel panelContenedor, Guid userId, string loginName, string password, int nroDocument,
                   string firstName, string lastName, string position, string mail, string address,
                   string telephone, bool isEmployee, int State)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var promotionRepo = Factory.Current.GetPromotionRepository();
            //var promotionService = new BLL.Service.PromotionService(promotionRepo);
            //_promotionSLService = new PromotionSLService(promotionService);


        }

        /// <summary>
        /// Constructor principal simplificado para carga normal del formulario.
        /// </summary>
        public MenuModProm(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor actual.
        /// </summary>
        /// <param name="formchild">Instancia del formulario hijo a mostrar.</param>
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
        /// Abre el formulario de búsqueda de promociones.
        /// </summary>
        private void btnFindPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPromotions(_panelContenedor));
        }

        /// <summary>
        /// Abre el formulario para registrar una nueva promoción.
        /// </summary>
        private void btnRegPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegProm(_panelContenedor));
        }

        /// <summary>
        /// Abre nuevamente este formulario para editar promociones.
        /// </summary>
        private void btnModPromotion_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModProm(_panelContenedor));
        }
    }
}

