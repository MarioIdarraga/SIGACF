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
using SL.Composite;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class MenuModPatent : Form
    {

        private Patente _patente;

        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;
        public MenuModPatent(Panel panelContenedor, Patente patente)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            _permissionSLService = new PermissionSLService();

            _patente = patente; 

            txtName.Text = _patente.Name;
            txtFormName.Text = _patente.FormName;
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

        private void btnFindPatent_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPatents(_panelContenedor));
        }

        private void btnRegPatent_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPatent(_panelContenedor));
        }

        private void btnModPatent_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var formName = txtFormName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("El nombre de la patente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Usás el objeto que recibiste para modificar
            _patente.Name = name;
            _patente.FormName = string.IsNullOrWhiteSpace(formName) ? null : formName;

            try
            {
                _permissionSLService.UpdatePatent(_patente);

                MessageBox.Show("Patente modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la patente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
