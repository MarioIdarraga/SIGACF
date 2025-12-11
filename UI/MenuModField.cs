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
    /// <summary>
    /// Formulario para modificar los datos de una cancha existente.
    /// Permite editar capacidad, costo horario, tipo de cancha y estado.
    /// </summary>
    public partial class MenuModField : Form
    {
        private Panel _panelContenedor;
        private readonly FieldSLService _fieldSLService;
        private Field _field;

        /// <summary>
        /// Constructor principal. Inicializa servicios, traducciones,
        /// carga combos y asigna los datos actuales del campo a modificar.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se insertan los formularios secundarios.</param>
        /// <param name="field">Objeto Field seleccionado para modificar.</param>
        public MenuModField(Panel panelContenedor, Field field)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            _field = field;

            this.Translate();

            var fieldRepo = DAL.Factory.Factory.Current.GetFieldRepository();
            var fieldService = new BLL.Service.FieldService(fieldRepo);
            _fieldSLService = new SL.Service.FieldSLService(fieldService);

            // Cargar combo de tipos
            CargarTiposDeCancha();

            txtName.Text = _field.Name;
            txtCapacity.Text = _field.Capacity.ToString();
            txtHourlyCost.Text = _field.HourlyCost.ToString();
            txtState.Text = _field.IdFieldState.ToString();
            cmbFieldType.SelectedItem = (FieldType)_field.FieldType;
        }

        /// <summary>
        /// Carga en el ComboBox la lista de tipos de cancha disponibles (enum FieldType).
        /// </summary>
        private void CargarTiposDeCancha()
        {
            cmbFieldType.DataSource = Enum.GetValues(typeof(FieldType));
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
        /// Navega al formulario de búsqueda de canchas.
        /// </summary>
        private void btnFindField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuFindFields(_panelContenedor));
        }

        /// <summary>
        /// Navega al formulario de registro de nuevas canchas.
        /// </summary>
        private void btnRegField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegField(_panelContenedor));
        }

        /// <summary>
        /// Valida los datos del formulario, actualiza el objeto Field,
        /// y llama a la capa SL para persistir los cambios.
        /// </summary>
        private void btnModField_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacity.Text) ||
                    string.IsNullOrWhiteSpace(txtState.Text) ||
                    cmbFieldType.SelectedItem == null)
                {
                    MessageBox.Show(
                        "Por favor, complete todos los campos obligatorios.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validación capacidad
                if (!int.TryParse(txtCapacity.Text.Trim(), out int capacity))
                {
                    MessageBox.Show(
                        "Capacidad inválida. Ingrese un número entero.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validación costo horario
                if (!decimal.TryParse(txtHourlyCost.Text.Trim(), out decimal hourlyCost))
                {
                    MessageBox.Show(
                        "Costo por hora inválido. Ingrese un número decimal válido.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Actualización del objeto original
                _field.Name = txtName.Text.Trim();
                _field.Capacity = capacity;
                _field.HourlyCost = hourlyCost;
                _field.FieldType = (int)cmbFieldType.SelectedValue;

                // ⚠️ Respeto tu código tal cual: asignás el Length del texto.
                _field.IdFieldState = txtState.Text.Length;

                // Persistencia mediante SL
                _fieldSLService.Update(_field);

                MessageBox.Show(
                    "Cancha modificada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al modificar la cancha: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

