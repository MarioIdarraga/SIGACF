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
    /// Formulario para la gestión de patentes (permisos simples).
    /// Permite registrar nuevas patentes y navegar hacia la búsqueda de patentes existentes.
    /// </summary>
    public partial class MenuRegPatent : Form
    {
        private Panel _panelContenedor;

        private readonly PermissionSLService _permissionSLService;

        /// <summary>
        /// Inicializa una nueva instancia del formulario MenuRegPatent.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se cargarán los formularios hijos.</param>
        public MenuRegPatent(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Traducción existente
            _permissionSLService = new PermissionSLService();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor.
        /// </summary>
        /// <param name="formchild">Instancia del formulario hijo a mostrar.</param>
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
        /// Registra una nueva patente valida los datos ingresados
        /// e invoca al servicio PermissionSLService para guardarla.
        /// </summary>
        private void btnRegPatent_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var formName = txtFormName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("El nombre de la patente es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var patente = new Patente
            {
                Name = name,
                FormName = string.IsNullOrWhiteSpace(formName) ? null : formName
            };

            try
            {
                _permissionSLService.SavePatent(patente);
                MessageBox.Show("Patente cargada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtFormName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la patente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

