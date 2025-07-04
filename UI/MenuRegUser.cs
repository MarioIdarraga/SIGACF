﻿using System;
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
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    public partial class MenuRegUser : Form
    {
        private Panel _panelContenedor;

        private readonly UserSLService _userSLService;

        public MenuRegUser(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Assuming you have a Translate method for localization
            var repo = Factory.Current.GetUserRepository();
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

        private void btnFindEmployee_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindUser(_panelContenedor));
        }

        private void btnModUser_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuModUser(_panelContenedor));
        }

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        private void btnApRegUser_Click(object sender, EventArgs e)
        {
            try
            {
             
                User newUser = new User
                {
                    UserId = Guid.NewGuid(),
                    LoginName = txtLoginName.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    NroDocument = int.Parse(txtNroDocument.Text.Trim()),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Position = txtPosition.Text.Trim(),
                    Mail = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Telephone = txtTelephone.Text.Trim(),
                    IsEmployee = chkIsEmployee.Checked,
                    State = int.Parse(txtState.Text.Trim())

                };

                // Validaciones 
                if (string.IsNullOrWhiteSpace(newUser.LoginName) ||
                    string.IsNullOrWhiteSpace(newUser.Password) ||
                    string.IsNullOrWhiteSpace(newUser.NroDocument.ToString()) ||
                    string.IsNullOrWhiteSpace(newUser.FirstName) ||
                    string.IsNullOrWhiteSpace(newUser.LastName) ||
                    string.IsNullOrWhiteSpace(newUser.Mail))
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _userSLService.Insert(newUser);

                MessageBox.Show("Usuario registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiamos los campos del formulario
                txtLoginName.Clear();
                txtPassword.Clear();        
                txtNroDocument.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtPosition.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                txtTelephone.Clear();
                txtState.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

