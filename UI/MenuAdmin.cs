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
using BLL.BusinessException;      
using BLL.Service;
using SL;
using SL.Service;                 
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario de menú de administración.
    /// Permite acceder a la administración de usuarios, canchas, promociones
    /// y al menú de backup del sistema.
    /// </summary>
    public partial class MenuAdmin : Form
    {
        private Panel _panelContenedor;
        private readonly ManagerAdminstration _managerAdminstration;
        private readonly string _defaultBackupDirectory;

        /// <summary>
        /// Constructor del menú de administración.
        /// Recibe el panel contenedor donde se cargarán los formularios hijos
        /// y establece la configuración inicial del módulo.
        /// </summary>
        /// <param name="panelContenedor">
        /// Panel principal sobre el que se mostrarán los formularios de administración.
        /// </param>
        public MenuAdmin(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _managerAdminstration = new ManagerAdminstration();
            _defaultBackupDirectory = ConfigurationManager.AppSettings["DefaultBackupDirectory"] ?? @"C:\SqlBackups";

            // Traducción de textos del formulario (soporte multi-idioma).
            this.Translate();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// removiendo previamente cualquier control existente.
        /// </summary>
        /// <param name="formchild">Instancia del formulario a mostrar.</param>
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
        /// Maneja el click sobre el botón de administración de usuarios/permisos.
        /// Abre el formulario de búsqueda de usuarios.
        /// </summary>
        private void btnAdminEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFormChild(new MenuFindUsers(_panelContenedor));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir el menú de usuarios: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el menú de usuarios. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el click sobre el botón de administración de canchas.
        /// Abre el formulario de búsqueda de canchas.
        /// </summary>
        private void btnAdminFields_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFormChild(new MenuFindFields(_panelContenedor));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir el menú de canchas: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el menú de canchas. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el click sobre el botón de administración de promociones.
        /// Abre el formulario de búsqueda de promociones.
        /// </summary>
        private void btnAdminProm_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFormChild(new MenuFindPromotions(_panelContenedor));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir el menú de promociones: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el menú de promociones. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el click sobre el botón de BackUp.
        /// Abre el formulario de administración de backups de la base de datos.
        /// </summary>
        private void btnAdminManuals_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFormChild(new MenuBackUp(_panelContenedor));
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir el menú de backup: {ex}",
                    System.Diagnostics.Tracing.EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el menú de backup. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
