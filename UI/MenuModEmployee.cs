﻿using System;
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
    public partial class MenuModEmployee : Form
    {
        private Panel _panelContenedor;

        public MenuModEmployee(Panel panelContenedor)
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

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        private void btnFindEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindEmployee(_panelContenedor));
        }

        private void btnRegEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegEmployee(_panelContenedor));
        }
    }
}
