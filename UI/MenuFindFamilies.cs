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
    /// <summary>
    /// Formulario responsable de realizar la búsqueda y visualización
    /// de familias de permisos dentro del sistema.
    /// 
    /// Permite:
    /// - Volver al menú de administración.
    /// - Registrar nuevas familias.
    /// - Modificar familias existentes.
    /// - Buscar todas las familias con sus permisos asociados.
    /// 
    /// Utiliza <see cref="PermissionSLService"/> para consultar la capa de servicios (SL).
    /// </summary>
    public partial class MenuFindFamilies : Form
    {
        /// <summary>
        /// Panel contenedor donde se cargan los formularios hijos.
        /// </summary>
        private Panel _panelContenedor;

        /// <summary>
        /// Servicio de permisos proveniente de la capa SL.
        /// Encargado de obtener, modificar y consultar familias.
        /// </summary>
        private readonly PermissionSLService _permissionSLService;

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="MenuFindFamilies"/>.
        /// </summary>
        /// <param name="Contenedor">Panel donde se cargarán los formularios hijos.</param>
        public MenuFindFamilies(Panel Contenedor)
        {
            InitializeComponent();
            _panelContenedor = Contenedor;

            // Traducción dinámica según idioma seleccionado
            this.Translate();

            _permissionSLService = new PermissionSLService();
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor principal.
        /// Reemplaza el contenido previamente existente.
        /// </summary>
        /// <param name="formchild">Formulario hijo a visualizar.</param>
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
        /// Evento del botón "Menu Administración".
        /// Regresa al menú principal de administración.
        /// </summary>
        /// <param name="sender">Objeto que dispara el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        /// <summary>
        /// Evento del botón "Registrar Familia".
        /// Abre el formulario de registro de nuevas familias.
        /// </summary>
        private void btnRegFamily_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegFamily(_panelContenedor));
        }

        /// <summary>
        /// Evento del botón "Modificar Familia".
        /// Abre el formulario de modificación de la familia seleccionada.
        /// 
        /// Requisitos:
        /// - Debe haber una fila seleccionada.
        /// - La familia debe existir en la lista obtenida desde SL.
        /// </summary>
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

        /// <summary>
        /// Evento del botón "Buscar Familias".
        /// Obtiene todas las familias registradas y las muestra en el <see cref="DataGridView"/>.
        /// 
        /// Maneja:
        /// - Cantidad de resultados.
        /// - Estado del label inferior.
        /// - Excepciones en la capa SL.
        /// </summary>
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

