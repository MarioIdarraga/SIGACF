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
    public partial class MenuManuals : Form
    {
        private Panel _panelContenedor;
        public MenuManuals(Panel panelContenedor)
        {

            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); //Traducir

        }

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
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
    }
}
