using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class MenuSales : Form
    {
        private Panel _panelContenedor;

        public MenuSales(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
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

        private void btnModBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModBooking(_panelContenedor));
        }

        private void btnRegBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegBooking(_panelContenedor));
        }

        private void btnRegPay_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPay(_panelContenedor));
        }

        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuCanBooking(_panelContenedor));
        }
    }
}
