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
    public partial class MenuModFamily : Form
    {

        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;

        private readonly Familia _familia;

        public MenuModFamily(Panel contenedor, Familia familia)
        {
            InitializeComponent();
            this.Translate();

            _panelContenedor = contenedor;
            _permissionSLService = new PermissionSLService();
            _familia = familia;

            // Mostrar datos actuales
            txtName.Text = _familia.Name;

            var patentes = _permissionSLService.GetAllPatents();
            checkedListPatent.DataSource = patentes;
            checkedListPatent.DisplayMember = "Name";

            // Marcar las patentes que ya tiene esta familia
            foreach (var patente in _familia.GetChildren().OfType<Patente>())
            {
                for (int i = 0; i < checkedListPatent.Items.Count; i++)
                {
                    if (checkedListPatent.Items[i] is Patente p && p.IdComponent == patente.IdComponent)
                    {
                        checkedListPatent.SetItemChecked(i, true);
                        listBoxAdd.Items.Add(p);
                    }
                }
            }
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
            OpenFormChild(new MenuRegFamily(_panelContenedor));
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

        private void btnModFamily_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas
            var nombreNuevo = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreNuevo))
            {
                MessageBox.Show("Debe ingresar un nombre para la familia.","Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxAdd.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una patente a la familia.","Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Reconstruir la familia en memoria
            _familia.Name = nombreNuevo;
            _familia.GetChildren().Clear();                // limpiamos hijos actuales

            foreach (var item in listBoxAdd.Items)
                if (item is Patente p) _familia.Add(p);

            // 3. Persistir cambios
            try
            {
                _permissionSLService.UpdateFamily(_familia);
                MessageBox.Show("Familia modificada con éxito.","Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: volver al listado
                OpenFormChild(new MenuFindFamilies(_panelContenedor));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la familia: {ex.Message}","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
