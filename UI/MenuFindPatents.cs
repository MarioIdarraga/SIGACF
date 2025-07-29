using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SL.Composite;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class MenuFindPatents : Form
    {

        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;

        public MenuFindPatents(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization

            _permissionSLService = new PermissionSLService();

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

        private void btnMenuUsers_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }
        private void btnRegPatent_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPatent(_panelContenedor));
        }

        private void btnModPatent_Click(object sender, EventArgs e)
        {
            //OpenFormChild(new MenuModPatent(_panelContenedor));
        }

        private void btnFindPatent_Click(object sender, EventArgs e)
        {
            try
            {
                var patents = _permissionSLService.GetAllPermissionComponents()
                                                  .OfType<Patente>()
                                                  .ToList();

                dataGridViewPatents.DataSource = patents.Select(p => new
                {
                    p.IdComponent,
                    p.Name,
                    p.FormName,
                    p.DVH
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar las patentes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
