using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Service;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using SL;
using SL.Composite;
using SL.Service;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario para registrar un nuevo usuario del sistema.
    /// Permite asignar una familia de permisos y cargar datos personales.
    /// </summary>
    public partial class MenuRegUser : Form
    {
        private Panel _panelContenedor;
        private readonly UserSLService _userSLService;

        /// <summary>
        /// Constructor principal. Inicializa componentes, carga familias de permisos
        /// y configura el traductor de interfaz.
        /// </summary>
        public MenuRegUser(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var userRepo = Factory.Current.GetUserRepository();
            var userService = new BLL.Service.UserService(userRepo);
            _userSLService = new UserSLService(userService);

            var familias = new PermissionSLService().GetAllFamilies();
            cmbFamily.DataSource = familias;
            cmbFamily.DisplayMember = "Name";
            cmbFamily.ValueMember = "IdComponent";
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// reemplazando el contenido previo.
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
        /// Navega al formulario de búsqueda de usuarios.
        /// </summary>
        private void btnFindEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUsers(_panelContenedor));
        }

        /// <summary>
        /// Navega al formulario de modificación de usuarios.
        /// </summary>
        private void btnModUser_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModUser(_panelContenedor));
        }

        /// <summary>
        /// Navega al menú de administración general.
        /// </summary>
        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema luego de validar los campos obligatorios.
        /// Asigna familia de permisos, persiste datos y limpia el formulario.
        /// </summary>
        private void btnApRegUser_Click(object sender, EventArgs e)
        {
            try
            {
                var famSeleccionada = cmbFamily.SelectedItem as Familia;
                if (famSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una familia de permisos.");
                    return;
                }

                User newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    LoginName = txtLoginName.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    NroDocument = int.Parse(txtNroDocument.Text.Trim()),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Position = famSeleccionada.Name,
                    Mail = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Telephone = txtTelephone.Text.Trim(),
                    State = 1
                };

                // Validaciones básicas del FrontEnd
                if (string.IsNullOrWhiteSpace(newUser.LoginName) ||
                    string.IsNullOrWhiteSpace(newUser.Password) ||
                    string.IsNullOrWhiteSpace(newUser.NroDocument.ToString()) ||
                    string.IsNullOrWhiteSpace(newUser.FirstName) ||
                    string.IsNullOrWhiteSpace(newUser.LastName) ||
                    string.IsNullOrWhiteSpace(newUser.Mail))
                {
                    MessageBox.Show(
                        "Por favor, complete los campos obligatorios.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                _userSLService.Insert(newUser, famSeleccionada.IdComponent);

                new PermissionSLService().AssignFamiliesToUser(
                    newUser.UserId,
                    new List<Guid> { famSeleccionada.IdComponent });

                MessageBox.Show(
                    "Usuario registrado con éxito, recuerde darle la contraseña al usuario nuevo, ya que deberá cambiarla en su primer login.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpiar formulario
                txtLoginName.Clear();
                txtPassword.Clear();
                txtNroDocument.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                txtTelephone.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ocurrió un error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}


