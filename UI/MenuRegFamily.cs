using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SL;
using SL.Composite;
using SL.Service;
using UI.Helpers;

namespace UI
{
    public partial class MenuRegFamily : Form
    {
        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;

        public MenuRegFamily(Panel Contenedor)
        {
            InitializeComponent();
            _panelContenedor = Contenedor;
            this.Translate();

            _permissionSLService = new PermissionSLService(); 

            var patentes = _permissionSLService.GetAllPatents();
            checkedListPatent.DataSource = patentes;
            checkedListPatent.DisplayMember = "Name";
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

        private void btnFindFamily_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindFamilies(_panelContenedor));
        }

        private void btnRegFamily_Click(object sender, EventArgs e)
        {
            var nombre = txtName.Text.Trim();

            // Validaciones básicas en la UI antes de delegar al SL
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Debe ingresar un nombre para la familia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxAdd.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una patente a la familia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var familia = new Familia { Name = nombre };

            foreach (var item in listBoxAdd.Items)
            {
                if (item is Patente patente)
                    familia.Add(patente);
            }

            try
            {
                _permissionSLService.SaveFamily(familia);
                MessageBox.Show("Familia registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                listBoxAdd.Items.Clear();
                txtName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la familia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListPatent.CheckedItems)
            {
                if (item is Patente patente && !listBoxAdd.Items.Contains(patente))
                {
                    listBoxAdd.Items.Add(patente);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var seleccionados = listBoxAdd.SelectedItems.Cast<object>().ToList();
            foreach (var item in seleccionados)
            {
                listBoxAdd.Items.Remove(item);
            }
        }
    }
}
