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
    /// <summary>
    /// Formulario para modificar una familia de permisos existente.
    /// Permite editar su nombre y administrar las patentes asociadas.
    /// </summary>
    public partial class MenuModFamily : Form
    {
        private Panel _panelContenedor;
        private readonly PermissionSLService _permissionSLService;
        private readonly Familia _familia;

        /// <summary>
        /// Constructor principal. Recibe la familia a modificar,
        /// carga sus datos actuales y marca las patentes asociadas.
        /// </summary>
        /// <param name="contenedor">Panel donde se cargan los subformularios.</param>
        /// <param name="familia">Familia seleccionada para modificar.</param>
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

            // Marcar patentes ya pertenecientes a la familia
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

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// Reemplaza cualquier formulario anterior.
        /// </summary>
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

        /// <summary>
        /// Navega al formulario de búsqueda de familias.
        /// </summary>
        private void btnFindFamily_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindFamilies(_panelContenedor));
        }

        /// <summary>
        /// Navega al formulario para registrar una nueva familia de permisos.
        /// </summary>
        private void btnRegFamily_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegFamily(_panelContenedor));
        }

        /// <summary>
        /// Agrega al listado las patentes seleccionadas en el CheckedListBox.
        /// </summary>
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

        /// <summary>
        /// Elimina patentes del listado según los ítems seleccionados.
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var seleccionados = listBoxAdd.SelectedItems.Cast<object>().ToList();

            foreach (var item in seleccionados)
            {
                listBoxAdd.Items.Remove(item);
            }
        }

        /// <summary>
        /// Valida los datos, reconstruye la familia con las patentes seleccionadas,
        /// y solicita la actualización a la capa SL.
        /// </summary>
        private void btnModFamily_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas
            var nombreNuevo = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreNuevo))
            {
                MessageBox.Show("Debe ingresar un nombre para la familia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBoxAdd.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una patente a la familia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Reconstruir familia
            _familia.Name = nombreNuevo;
            _familia.GetChildren().Clear();

            foreach (var item in listBoxAdd.Items)
            {
                if (item is Patente p)
                    _familia.Add(p);
            }

            try
            {
                _permissionSLService.UpdateFamily(_familia);
                MessageBox.Show("Familia modificada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OpenFormChild(new MenuFindFamilies(_panelContenedor));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la familia: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
