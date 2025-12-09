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
using BLL.Service;
using DAL.Factory;
using Domain;
using SL;
using SL.Service;
using SL.Service.Extension;
using UI.Helpers;

namespace UI
{
    public partial class MenuModField : Form
    {
        private Panel _panelContenedor;

        private readonly FieldSLService _fieldSLService;

        private Field _field;

        public MenuModField(Panel panelContenedor, Field field)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _field = field;

            this.Translate();

            var fieldRepo = DAL.Factory.Factory.Current.GetFieldRepository();
            var fieldService = new BLL.Service.FieldService(fieldRepo);
            _fieldSLService = new SL.Service.FieldSLService(fieldService);

            // Cargar combos
            CargarTiposDeCancha();

            txtName.Text = _field.Name;
            txtCapacity.Text = _field.Capacity.ToString();
            txtHourlyCost.Text = _field.HourlyCost.ToString();
            txtState.Text = _field.IdFieldState.ToString();
            cmbFieldType.SelectedItem = (FieldType)_field.FieldType;
        }

        private void CargarTiposDeCancha()
        {
            cmbFieldType.DataSource = Enum.GetValues(typeof(FieldType));
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
            try
            {
                // Validaciones de campos requeridos
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacity.Text) ||
                    string.IsNullOrWhiteSpace(txtState.Text) ||

                    cmbFieldType.SelectedItem == null )
                    
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación numérica de capacidad
                if (!int.TryParse(txtCapacity.Text.Trim(), out int capacity))
                {
                    MessageBox.Show("Capacidad inválida. Ingrese un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación numérica de costo
                if (!decimal.TryParse(txtHourlyCost.Text.Trim(), out decimal hourlyCost))
                {
                    MessageBox.Show("Costo por hora inválido. Ingrese un número decimal válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Actualizar los valores del objeto original (_field)
                _field.Name = txtName.Text.Trim();
                _field.Capacity = capacity;
                _field.HourlyCost = hourlyCost;
                _field.FieldType = (int)cmbFieldType.SelectedValue;
                _field.IdFieldState = txtState.Text.Length;

                // Llamada a la SL
                _fieldSLService.Update(_field);

                MessageBox.Show("Cancha modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la cancha: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
