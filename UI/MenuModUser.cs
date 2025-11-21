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
using SL.Composite;
using SL.Service.Extension;
using UI.Helpers;
using BLL.BusinessException;
using SL.Service;
using System.Diagnostics.Tracing;

namespace UI
{
    /// <summary>
    /// Formulario para modificar un usuario existente.
    /// Permite editar los datos personales, credenciales, familia (perfil)
    /// y estado del usuario seleccionado.
    /// </summary>
    public partial class MenuModUser : Form
    {
        private Panel _panelContenedor;

        private readonly UserSLService _userSLService;

        // Id del usuario a modificar (solo en memoria, no se muestra en la UI)
        private readonly Guid _userId;

        /// <summary>
        /// Constructor principal del formulario de modificación de usuario.
        /// Recibe el panel contenedor y los datos del usuario a editar.
        /// </summary>
        public MenuModUser(
            Panel panelContenedor,
            Guid userId,
            string loginName,
            string password,
            int nroDocument,
            string firstName,
            string lastName,
            string position,
            string mail,
            string address,
            string telephone,
            bool isEmployee,
            int State)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate(); // Localización

            var userRepo = Factory.Current.GetUserRepository();
            var userService = new BLL.Service.UserService(userRepo);
            _userSLService = new UserSLService(userService);

            // Guardar el Id internamente (no hay textbox para el Id)
            _userId = userId;

            // Llenar los campos del formulario con los datos del usuario
            txtLoginName.Text = loginName;
            txtPassword.Text = password;
            txtNroDocument.Text = nroDocument.ToString();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            cmbFamily.Text = position;
            txtMail.Text = mail;
            txtAddress.Text = address;
            txtTelephone.Text = telephone;
            chkIsEmployee.Checked = isEmployee;
            txtState.Text = State.ToString();
        }

        /// <summary>
        /// Constructor alternativo que solo recibe el panel contenedor.
        /// Usado si se instancia el formulario sin usuario seleccionado.
        /// </summary>
        public MenuModUser(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate();

            var userRepo = Factory.Current.GetUserRepository();
            var userService = new BLL.Service.UserService(userRepo);
            _userSLService = new UserSLService(userService);

            // No tenemos aún usuario -> Guid.Empty
            _userId = Guid.Empty;
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// removiendo cualquier control existente.
        /// </summary>
        private void OpenFormChild(object formchild)
        {
            try
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
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al abrir formulario hijo desde MenuModUser: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al abrir el formulario. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
        /// Navega a la pantalla de búsqueda de usuarios.
        /// </summary>
        private void btnFindEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }

        /// <summary>
        /// Navega a la pantalla de registro de usuario.
        /// </summary>
        private void btnRegEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegUser(_panelContenedor));
        }

        /// <summary>
        /// Toma los datos del formulario y realiza la modificación del usuario
        /// llamando a la capa de servicios (SL).
        /// </summary>
        private void btnModUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos requeridos estén completos
                if (string.IsNullOrWhiteSpace(txtLoginName.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtNroDocument.Text) ||
                    string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(cmbFamily.Text) ||
                    string.IsNullOrWhiteSpace(txtMail.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone.Text) ||
                    string.IsNullOrWhiteSpace(txtState.Text))
                {
                    MessageBox.Show(
                        "Por favor, complete todos los campos antes de modificar el usuario.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validar que tengamos un Id de usuario válido
                if (_userId == Guid.Empty)
                    throw new BusinessException("No se encontró el identificador del usuario a modificar.");

                // Convertir campos numéricos
                if (!int.TryParse(txtNroDocument.Text, out int nroDoc))
                    throw new BusinessException("El número de documento no es válido.");

                if (!int.TryParse(txtState.Text, out int state))
                    throw new BusinessException("El estado del usuario no es válido.");

                if (!(cmbFamily.SelectedItem is PermissionComponent famSeleccionada))
                    throw new BusinessException("Debe seleccionar un cargo/posición válido.");

                Guid familyId = famSeleccionada.IdComponent;

                // Crear objeto User con los datos del formulario
                User updatedUser = new User
                {
                    UserId = _userId, // usamos el Id interno
                    LoginName = txtLoginName.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    NroDocument = nroDoc,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Position = famSeleccionada.Name,
                    Mail = txtMail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Telephone = txtTelephone.Text.Trim(),
                    State = state,
                    IsEmployee = chkIsEmployee.Checked
                };

                _userSLService.Update(updatedUser, familyId);

                MessageBox.Show(
                    "Usuario modificado con éxito.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
                    $"Error inesperado al modificar el usuario: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al modificar el usuario. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}


