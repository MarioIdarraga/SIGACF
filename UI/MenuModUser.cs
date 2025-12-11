using BLL.BusinessException;
using DAL.Factory;
using Domain;
using SL;
using SL.Composite;
using SL.Helpers;
using SL.Service;
using System;
using System.Diagnostics.Tracing;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para modificar un usuario existente.
    /// Permite actualizar datos personales, credenciales, cargo y estado.
    /// </summary>
    public partial class MenuModUser : Form
    {
        private readonly Panel _panelContenedor;
        private readonly UserSLService _userSLService;
        private readonly Guid _userId;

        /// <summary>
        /// Constructor utilizado cuando el usuario proviene de la pantalla de búsqueda.
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
            int state,
            bool isEmployee)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate();

            var repo = Factory.Current.GetUserRepository();
            var svc = new BLL.Service.UserService(repo);
            _userSLService = new UserSLService(svc);

            _userId = userId;

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
            txtState.Text = state.ToString();

            LoadFamilies();
        }

        /// <summary>
        /// Constructor alternativo sin datos iniciales.
        /// </summary>
        public MenuModUser(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate();

            var repo = Factory.Current.GetUserRepository();
            var svc = new BLL.Service.UserService(repo);
            _userSLService = new UserSLService(svc);

            _userId = Guid.Empty;
        }

        /// <summary>
        /// Abre un formulario hijo dentro del contenedor.
        /// </summary>
        private void OpenFormChild(Form formchild)
        {
            try
            {
                if (_panelContenedor.Controls.Count > 0)
                    _panelContenedor.Controls.RemoveAt(0);

                formchild.TopLevel = false;
                formchild.Dock = DockStyle.Fill;
                _panelContenedor.Controls.Add(formchild);
                _panelContenedor.Tag = formchild;
                formchild.Show();
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al abrir formulario hijo: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error al abrir la pantalla solicitada.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        private void btnMenuFindEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }

        private void btnMenuRegUser_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegUser(_panelContenedor));
        }

        /// <summary>
        /// Valida la entrada y envía la actualización del usuario hacia la capa SL.
        /// </summary>
        /// <summary>
        /// Valida la entrada y envía la actualización del usuario hacia la capa SL.
        /// </summary>
        private void btnModUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (_userId == Guid.Empty)
                    throw new BusinessException("No se encontró el usuario a modificar.");

                if (!int.TryParse(txtNroDocument.Text.Trim(), out int nroDoc))
                    throw new BusinessException("El número de documento no es válido.");

                if (!int.TryParse(txtState.Text.Trim(), out int newState))
                    throw new BusinessException("El estado no es válido.");

                var famSeleccionada = cmbFamily.SelectedItem as PermissionComponent;
                if (famSeleccionada == null)
                    throw new BusinessException("Debe seleccionar un cargo/posición válido.");

                var updatedUser = new User
                {
                    UserId = _userId,
                    LoginName = txtLoginName.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    NroDocument = nroDoc,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Position = famSeleccionada.Name,
                    Mail = txtMail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Telephone = txtTelephone.Text.Trim(),
                    State = newState,
                    IsEmployee = chkIsEmployee.Checked
                };

                _userSLService.Update(updatedUser, famSeleccionada.IdComponent);

                MessageBox.Show(
                    "Usuario modificado con éxito.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (BusinessException ex)
            {
                
                LoggerService.Log(ex.Message, EventLevel.Warning, Session.CurrentUser?.LoginName);

                MessageBox.Show(
                    ex.Message,
                    "Advertencia de Negocio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (ArgumentException ax) 
            {
                
                LoggerService.Log(ax.Message, EventLevel.Warning, Session.CurrentUser?.LoginName);

                MessageBox.Show(
                    ax.Message,
                    "Error de Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                
                LoggerService.Log(
                    $"Error inesperado al modificar usuario: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                MessageBox.Show(
                    "Ocurrió un error inesperado al modificar el usuario. Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga los perfiles/familias disponibles.
        /// </summary>
        private void LoadFamilies()
        {
            try
            {
                var permissionSL = new PermissionSLService();
                var families = permissionSL.GetAllFamilies();

                cmbFamily.DataSource = families;
                cmbFamily.DisplayMember = "Name";
                cmbFamily.ValueMember = "IdComponent";
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al cargar familias: {ex}", EventLevel.Error);

                MessageBox.Show(
                    "Ocurrió un error al cargar los cargos/posiciones.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMenuFindUser_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }

        private void btnMenuRegUser_Click_1(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegUser(_panelContenedor));
        }
    }
}
