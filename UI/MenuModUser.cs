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

namespace UI
{
    public partial class MenuModUser : Form
    {
        private Panel _panelContenedor;

        private readonly UserSLService _userSLService;


        public MenuModUser(Panel panelContenedor, Guid userId, string loginName, string password, int nroDocument,
                   string firstName, string lastName, string position, string mail, string address,
                   string telephone, bool isEmployee, int State)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate(); // Assuming you have a Translate method for localization

            var userRepo = Factory.Current.GetUserRepository();
            var userService = new BLL.Service.UserService(userRepo);
            _userSLService = new UserSLService(userService);

            // Llenar los campos del formulario con los datos del usuario
            txtUserId.Text = userId.ToString();
            txtLoginName.Text = loginName;
            txtPassword.Text = password;
            txtNroDocument.Text = nroDocument.ToString();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtPosition.Text = position;
            txtMail.Text = mail;
            txtAddress.Text = address;
            txtTelephone.Text = telephone;
            chkIsEmployee.Checked = isEmployee; 
            txtState.Text = State.ToString();
        }

        public MenuModUser(Panel panelContenedor)
        {
            _panelContenedor = panelContenedor;
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

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        private void btnFindEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUser(_panelContenedor));
        }

        private void btnRegEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegUser(_panelContenedor));
        }

        private void btnModUser_Click(object sender, EventArgs e)
        {

            try
            {
                // Validar que todos los campos requeridos estén completos
                if (string.IsNullOrWhiteSpace(txtUserId.Text) ||
                    string.IsNullOrWhiteSpace(txtLoginName.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtNroDocument.Text) ||
                    string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(txtPosition.Text) ||
                    string.IsNullOrWhiteSpace(txtMail.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone.Text) ||
                    string.IsNullOrWhiteSpace(txtState.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de modificar el usuario.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear objeto User con los datos del formulario
                User updatedUser = new User
                {
                    UserId = Guid.Parse(txtUserId.Text),
                    LoginName = txtLoginName.Text,
                    Password = txtPassword.Text,
                    NroDocument = int.Parse(txtNroDocument.Text),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Position = txtPosition.Text,
                    Mail = txtMail.Text,
                    Address = txtAddress.Text,
                    Telephone = txtTelephone.Text,
                    State = int.Parse(txtState.Text)
                };

                // Llamar al método Update de la SL
                _userSLService.Update(updatedUser);

                MessageBox.Show("Usuario modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

