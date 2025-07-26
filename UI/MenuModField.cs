using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Factory;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    public partial class MenuModField : Form
    {
        private Panel _panelContenedor;

        //private readonly FieldSLService _fieldSLService;


        public MenuModField(Panel panelContenedor, Guid userId, string loginName, string password, int nroDocument,
                   string firstName, string lastName, string position, string mail, string address,
                   string telephone, bool isEmployee, int State)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate(); // Traductor

            var fieldRepo = Factory.Current.GetFieldRepository();
            //var fieldService = new BLL.Service.FieldService(fieldRepo);
            //_fieldSLService = new FieldSLService(fieldService);

            // Llenar los campos del formulario con los datos del usuario
            //txtUserId.Text = userId.ToString();
            //txtLoginName.Text = loginName;
            //txtPassword.Text = password;
            //txtNroDocument.Text = nroDocument.ToString();
            //txtFirstName.Text = firstName;
            //txtLastName.Text = lastName;
            //txtPosition.Text = position;
            //txtMail.Text = mail;
            //txtAddress.Text = address;
            //txtTelephone.Text = telephone;
            //chkIsEmployee.Checked = isEmployee;
            //txtState.Text = State.ToString();
        }

        public MenuModField(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;

            this.Translate();
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
        private void btnFindField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindFields(_panelContenedor));
        }

        private void btnRegField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegField(_panelContenedor));
        }

        private void btnModField_Click(object sender, EventArgs e)
        {

        }
    }
}
