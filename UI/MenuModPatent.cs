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
    /// <summary>
    /// Formulario para la modificación de patentes existentes.
    /// Permite editar el nombre y formulario asociado de una patente previamente seleccionada.
    /// </summary>
    public partial class MenuModPatent : Form
    {
        private Patente _patente;
        private Panel _panelContenedor;
        private readonly PermissionSLService _permissionSLService;

        /// <summary>
        /// Constructor que recibe la patente a modificar y configura el formulario con sus datos actuales.
        /// </summary>
        /// <param name="panelContenedor">Panel padre donde se cargan los formularios hijos.</param>
        /// <param name="patente">Objeto patente seleccionado que será modificado.</param>
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

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
        /// <param name="formchild">Instancia del formulario hijo a visualizar.</param>
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
        /// Abre la pantalla de búsqueda de patentes.
        /// </summary>
        private void btnFindPatent_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPatents(_panelContenedor));
        }

        /// <summary>
        /// Abre la pantalla de registro de una nueva patente.
        /// </summary>
        private void btnRegPatent_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegPatent(_panelContenedor));
        }

        /// <summary>
        /// Valida los datos ingresados y modifica la patente mediante PermissionSLService.
        /// </summary>
        private void btnModPatent_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var formName = txtFormName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("El nombre de la patente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

