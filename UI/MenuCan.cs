﻿using System;
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
    public partial class MenuCan : Form
    {
        private Panel _panelContenedor;

        public MenuCan(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization
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

        private void btnCanBooking_Click(object sender, EventArgs e)
        {
            //OpenFormChild(new MenuCanBooking(_panelContenedor));
        }

        private void btnFindBooking_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuSales(_panelContenedor));
        }

        private void btnFindCan_Click(object sender, EventArgs e)
        {

        }
    }
}
