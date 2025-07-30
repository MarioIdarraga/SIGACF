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
using SL;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class MenuFindFamilies : Form
    {

        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;
        public MenuFindFamilies(Panel Contenedor)
        {
            InitializeComponent();
            _panelContenedor = Contenedor;
            this.Translate();

            _permissionSLService = new PermissionSLService(); ;
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

        private void btnRegFamily_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegFamily(_panelContenedor));
        }

        private void btnModFamily_Click(object sender, EventArgs e)
        {
            if (dataGridViewFamilies.SelectedRows.Count > 0)
            {
                string nombre = dataGridViewFamilies.SelectedRows[0].Cells["Nombre"].Value.ToString();
                var familia = _permissionSLService.GetAllFamilies().FirstOrDefault(f => f.Name == nombre);
                if (familia != null)
                {
                    OpenFormChild(new MenuModFamily(_panelContenedor, familia));
                }
                else
                {
                    MessageBox.Show("No se encontró la familia seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnFindFamily_Click(object sender, EventArgs e)
        {
            try
            {
                var familias = _permissionSLService.GetAllFamilies();

                if (familias == null || familias.Count == 0)
                {
                    lblStatus.Text = "No se encontraron familias.";
                    dataGridViewFamilies.DataSource = null;
                    return;
                }

                var bindingList = familias.Select(f => new
                {
                    Nombre = f.Name,
                    CantidadDePermisos = f.GetChild()
                }).ToList();

                dataGridViewFamilies.DataSource = bindingList;
                lblStatus.Text = $"Se encontraron {bindingList.Count} familia(s).";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar familias: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
