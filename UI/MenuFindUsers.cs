using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using SL.Service.Extension;
using UI.Helpers;
using BLL.BusinessException;
using SL.Service;
using SL.BLL;
using System.Diagnostics.Tracing;

namespace UI
{
    /// <summary>
    /// Formulario de búsqueda y administración de usuarios.
    /// Permite filtrar usuarios por diferentes criterios, visualizar los resultados
    /// en un DataGridView, registrar nuevos usuarios y modificarlos.
    /// </summary>
    public partial class MenuFindUsers : Form
    {
        private readonly UserSLService _userSLService;

        private Panel _panelContenedor;

        /// <summary>
        /// Constructor del formulario de búsqueda de usuarios.
        /// Inicializa el panel contenedor, traduce el formulario
        /// e instancia el servicio de usuarios de la capa SL.
        /// </summary>
        /// <param name="panelContenedor">Panel principal donde se incrusta el formulario.</param>
        public MenuFindUsers(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); //Traducir

            var userRepo = Factory.Current.GetUserRepository();
            var userService = new BLL.Service.UserService(userRepo);
            _userSLService = new UserSLService(userService);
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// removiendo cualquier control existente.
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
        /// Oculta columnas técnicas del grid que el usuario no debe ver
        /// (por ejemplo, el identificador interno del usuario).
        /// </summary>
        private void HideTechnicalColumns()
        {
            if (dataGridViewUsers.Columns.Contains("UserId"))
                dataGridViewUsers.Columns["UserId"].Visible = false;
            if (dataGridViewUsers.Columns.Contains("Password"))
                dataGridViewUsers.Columns["Password"].Visible = false;
            if (dataGridViewUsers.Columns.Contains("DVH"))
                dataGridViewUsers.Columns["DVH"].Visible = false;
            if (dataGridViewUsers.Columns.Contains("ResetToken"))
                dataGridViewUsers.Columns["ResetToken"].Visible = false;

        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewUsers.Columns)
            {
                // Saltar columnas técnicas
                if (col.Name == "UserId")
                    continue;

                try
                {
                    // Claves esperadas en los archivos de idioma:
                    // LoginName, Password, NroDocument, FirstName, LastName,
                    // Position, Mail, Address, Telephone, IsEmployee, State, etc.
                    col.HeaderText = LanguageBLL.Current.Traductor(col.Name);
                }
                catch
                {
                    // Si no se encuentra la traducción, se deja el HeaderText tal cual.
                }
            }
        }

        /// <summary>
        /// Vuelve al menú de administración.
        /// </summary>
        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        /// <summary>
        /// Navega al formulario de registro de usuarios.
        /// </summary>
        private void btnRegUser_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegUser(_panelContenedor));
        }

        /// <summary>
        /// Abre el formulario de modificación de usuario
        /// usando el usuario seleccionado en el grid.
        /// </summary>
        private void btnModUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si hay filas en el DataGridView
                if (dataGridViewUsers.Rows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario de la lista para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar si hay una fila seleccionada
                if (dataGridViewUsers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un usuario para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener datos de la fila seleccionada
                DataGridViewRow selectedRow = dataGridViewUsers.SelectedRows[0];

                // Validar que las celdas no sean nulas
                if (selectedRow.Cells["UserId"].Value == null ||
                    selectedRow.Cells["LoginName"].Value == null ||
                    selectedRow.Cells["Password"].Value == null ||
                    selectedRow.Cells["NroDocument"].Value == null ||
                    selectedRow.Cells["FirstName"].Value == null ||
                    selectedRow.Cells["LastName"].Value == null ||
                    selectedRow.Cells["Position"].Value == null ||
                    selectedRow.Cells["Telephone"].Value == null ||
                    selectedRow.Cells["Mail"].Value == null ||
                    selectedRow.Cells["Address"].Value == null ||
                    selectedRow.Cells["IsEmployee"].Value == null ||
                    selectedRow.Cells["State"].Value == null)
                {
                    MessageBox.Show("El usuario seleccionado tiene datos inválidos. Intente seleccionar otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Extraer valores de la fila
                Guid userId = (Guid)selectedRow.Cells["UserId"].Value;
                string loginName = selectedRow.Cells["LoginName"].Value.ToString();
                string password = selectedRow.Cells["Password"].Value.ToString();
                int nroDocument = Convert.ToInt32(selectedRow.Cells["NroDocument"].Value);
                string firstName = selectedRow.Cells["FirstName"].Value.ToString();
                string lastName = selectedRow.Cells["LastName"].Value.ToString();
                string position = selectedRow.Cells["Position"].Value.ToString();
                string telephone = selectedRow.Cells["Telephone"].Value.ToString();
                string mail = selectedRow.Cells["Mail"].Value.ToString();
                string address = selectedRow.Cells["Address"].Value.ToString();
                bool isEmployee = Convert.ToBoolean(selectedRow.Cells["IsEmployee"].Value);
                int state = Convert.ToInt32(selectedRow.Cells["State"].Value);

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModUser(_panelContenedor, userId, loginName, password, nroDocument, firstName, lastName, position, mail, address, telephone, isEmployee, state));
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
                    $"Error inesperado al intentar modificar el usuario: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al intentar modificar el usuario. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Busca usuarios según los filtros ingresados
        /// y muestra el resultado en el DataGridView.
        /// </summary>
        private void btnFindUser_Click(object sender, EventArgs e)
        {
            int? nroDocumento = null;

            if (!string.IsNullOrWhiteSpace(txtNroDocument.Text))
            {
                if (int.TryParse(txtNroDocument.Text, out int result))
                {
                    nroDocumento = result;
                }
                else
                {
                    MessageBox.Show("Ingrese un número de documento válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Validaciones del FrontEnd
            string firstName = string.IsNullOrWhiteSpace(txtFirstName.Text) ? null : txtFirstName.Text.Trim();
            string lastName = string.IsNullOrWhiteSpace(txtLastName.Text) ? null : txtLastName.Text.Trim();
            string telephone = string.IsNullOrWhiteSpace(txtTelephone.Text) ? null : txtTelephone.Text.Trim();
            string mail = string.IsNullOrWhiteSpace(txtMail.Text) ? null : txtMail.Text.Trim();

            try
            {
                var users = _userSLService.GetAll(nroDocumento, firstName, lastName, telephone, mail);

                // Mostrar resultados en un DataGridView
                dataGridViewUsers.DataSource = users.ToList();

                // Ocultar columnas técnicas y traducir encabezados
                HideTechnicalColumns();
                TranslateGridHeaders();

                // Mensaje en la UI
                lblStatus.Text = users.Any()
                    ? $"Se encontraron {users.Count()} usuarios."
                    : "No se encontraron usuarios con esos criterios.";
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
                    $"Error inesperado al buscar usuarios: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al buscar usuarios. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Navega al menú de Familias (perfiles de permisos).
        /// </summary>
        private void btnFindFamilies_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindFamilies(_panelContenedor));
        }

        /// <summary>
        /// Navega al menú de Patentes (permisos individuales).
        /// </summary>
        private void btnFindPatents_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindPatents(_panelContenedor));
        }
    }
}

