using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Service;
using UI.Helpers;

namespace UI
{
    public partial class MenuAdmin : Form
    {
        private Panel _panelContenedor;
        private readonly ManagerAdminstration _managerAdminstration;
        private readonly string _defaultBackupDirectory;

        public MenuAdmin(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _managerAdminstration = new ManagerAdminstration();
            _defaultBackupDirectory = ConfigurationManager.AppSettings["DefaultBackupDirectory"] ?? @"C:\SqlBackups";
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

        private void btnAdminEmployees_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }

        private void btnAdminFields_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindFields(_panelContenedor));
        }

        private void btnAdminProm_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPromotions(_panelContenedor));
        }
    }
}
