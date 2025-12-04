using BLL.BusinessException;
using BLL.Service;
using DAL.Factory;
using Domain;
using SL;
using SL.BLL;
using SL.Service;
using SL.Service.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario de búsqueda y administración de canchas.
    /// Permite filtrar canchas por tipo y estado, visualizar los resultados
    /// en un DataGridView, registrar nuevas canchas y modificarlas.
    /// </summary>
    public partial class MenuFindFields : Form
    {
        private readonly Panel _panelContenedor;

        private readonly FieldSLService _fieldSLService;

        /// <summary>
        /// Constructor del formulario de búsqueda de canchas.
        /// Inicializa el panel contenedor, traduce el formulario,
        /// instancia el servicio de canchas de la capa SL
        /// y carga el combo de tipos de cancha.
        /// </summary>
        /// <param name="panelContenedor">Panel principal donde se muestra el formulario.</param>
        public MenuFindFields(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate(); // Traductor

            var fieldRepo = Factory.Current.GetFieldRepository();
            var fieldService = new FieldService(fieldRepo);
            _fieldSLService = new FieldSLService(fieldService);

            CargarTiposDeCancha();
        }

        /// <summary>
        /// Carga el combo de tipos de cancha a partir del enum FieldType.
        /// </summary>
        private void CargarTiposDeCancha()
        {
            try
            {
                cmbFieldType.DataSource = Enum.GetValues(typeof(FieldType));
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
                    $"Error inesperado al cargar los tipos de cancha: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al cargar los tipos de cancha. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Abre un formulario hijo dentro del panel contenedor,
        /// removiendo cualquier control existente.
        /// </summary>
        /// <param name="formchild">Formulario hijo a mostrar.</param>
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
                    $"Error inesperado al abrir formulario hijo: {ex}",
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
        /// Oculta las columnas técnicas (IdField, DVH) del grid,
        /// para que el usuario no vea identificadores internos.
        /// </summary>
        private void HideTechnicalColumns()
        {
            if (dataGridViewFields.Columns.Contains("IdField"))
                dataGridViewFields.Columns["IdField"].Visible = false;

            if (dataGridViewFields.Columns.Contains("DVH"))
                dataGridViewFields.Columns["DVH"].Visible = false;
        }

        /// <summary>
        /// Vuelve al menú de administración.
        /// </summary>
        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuAdmin(_panelContenedor));
        }

        /// <summary>
        /// Navega al formulario para registrar una nueva cancha.
        /// </summary>
        private void btnRegField_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRegField(_panelContenedor));
        }

        /// <summary>
        /// Abre el formulario de modificación de cancha
        /// usando la cancha seleccionada en el grid.
        /// </summary>
        private void btnModField_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewFields.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Seleccione una cancha para modificar.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var selectedField = dataGridViewFields.SelectedRows[0].DataBoundItem as Field;
                if (selectedField == null)
                {
                    MessageBox.Show(
                        "La cancha seleccionada no es válida.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                OpenFormChild(new MenuModField(_panelContenedor, selectedField));
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
                    $"Error inesperado al intentar modificar la cancha: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al intentar modificar la cancha. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridViewFields.Columns)
            {
                // Saltar columnas técnicas
                if (col.Name == "UserId")
                    continue;

                try
                {
                    col.HeaderText = LanguageBLL.Current.Traductor(col.Name);
                }
                catch
                {
                    // Si no se encuentra la traducción, se deja el HeaderText tal cual.
                }
            }
        }


        /// <summary>
        /// Ejecuta la búsqueda de canchas según tipo y estado seleccionados,
        /// y muestra el resultado en el DataGridView.
        /// </summary>
        private void btnFindField_Click(object sender, EventArgs e)
        {
            int? fieldType = null;
            if (!string.IsNullOrWhiteSpace(cmbFieldType.Text) &&
                int.TryParse(cmbFieldType.Text, out int tipo))
            {
                fieldType = tipo;
            }

            int? fieldState = null;
            if (cmbState.SelectedItem != null)
            {
                // Aquí asumimos que el SelectedValue es un entero que representa el estado
                fieldState = int.Parse(cmbState.SelectedValue.ToString());
            }

            try
            {
                var fields = _fieldSLService.GetAll(
                    name: null,
                    capacity: null,
                    fieldType: fieldType,
                    fieldState: fieldState); 

                dataGridViewFields.DataSource = fields.ToList();

                // Ocultar columnas técnicas
                HideTechnicalColumns();
                TranslateGridHeaders();

                lblStatus.Text = fields.Any()
                    ? $"Se encontraron {fields.Count()} canchas."
                    : "No se encontraron canchas con esos criterios.";
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
                    $"Error inesperado al buscar canchas: {ex}",
                    EventLevel.Critical);

                MessageBox.Show(
                    "Ocurrió un error inesperado al buscar canchas. " +
                    "Intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

