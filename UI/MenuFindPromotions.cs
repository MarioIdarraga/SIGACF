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
    public partial class MenuFindPromotions : Form
    {
        private Panel _panelContenedor;

        public MenuFindPromotions(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization

            var promotionRepo = Factory.Current.GetPromotionRepository();
            //var promotionService = new BLL.Service.PromotionService(promotionRepo);
            //_promotionSLService = new PromotionSLService(promotionService);
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

        }
    }
}
