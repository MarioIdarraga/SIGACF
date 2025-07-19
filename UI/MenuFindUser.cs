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
    public partial class MenuFindUser : Form
    {

        private readonly UserSLService _userSLService;

        private Panel _panelContenedor;

        public MenuFindUser(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization

            var userRepo = Factory.Current.GetUserRepository();
            var userService = new BLL.Service.UserService(userRepo);
            _userSLService = new UserSLService(userService);
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

        private void btnRegUser_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegUser(_panelContenedor));
        }

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
                    selectedRow.Cells["Mail"].Value == null ||
                    selectedRow.Cells["Address"].Value == null ||
                    selectedRow.Cells["Telephone"].Value == null |
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
                string mail = selectedRow.Cells["Mail"].Value.ToString();
                string address = selectedRow.Cells["Address"].Value.ToString();
                string telephone = selectedRow.Cells["Telephone"].Value.ToString();
                bool isEmployee = Convert.ToBoolean(selectedRow.Cells["IsEmployee"].Value);
                int state = Convert.ToInt32(selectedRow.Cells["State"].Value);

                // Abrir el formulario de modificación pasando los datos
                OpenFormChild(new MenuModUser(_panelContenedor, userId, loginName, password, nroDocument, firstName, lastName, position, mail, address, telephone, isEmployee, state));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar modificar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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

            //Validaciones del FrontEnd
            string firstName = string.IsNullOrWhiteSpace(txtFirstName.Text) ? null : txtFirstName.Text.Trim();
            string lastName = string.IsNullOrWhiteSpace(txtLastName.Text) ? null : txtLastName.Text.Trim();
            string telephone = string.IsNullOrWhiteSpace(txtTelephone.Text) ? null : txtTelephone.Text.Trim();
            string mail = string.IsNullOrWhiteSpace(txtMail.Text) ? null : txtMail.Text.Trim();

            try
            {

                var users = _userSLService.GetAll(nroDocumento, firstName, lastName, telephone, mail);

                // Mostrar resultados en un DataGridView
                dataGridViewUsers.DataSource = users.ToList();

                // Mensaje en la UI
                lblStatus.Text = users.Any()
                    ? $"Se encontraron {users.Count()} clientes."
                    : "No se encontraron clientes con esos criterios.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
