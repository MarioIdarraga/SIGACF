using BLL.Service;
using DAL.Factory;
using Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SL;
using SL.BLL;
using SL.Service;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UI.Helpers;

namespace UI
{
    /// <summary>
    /// Formulario de reporte de reservas por rango de fechas.
    /// Permite visualizar reservas y generar un archivo PDF.
    /// </summary>
    public partial class MenuRepBooking : Form
    {
        private readonly Panel _panelContenedor;
        private readonly BookingSLService _bookingSLService;
        private readonly BookingStateSLService _bookingStateSLService;

        /// <summary>
        /// Constructor que inicializa componentes y servicios de la capa SL.
        /// </summary>
        /// <param name="panelContenedor">Panel donde se incrusta el formulario.</param>
        public MenuRepBooking(Panel panelContenedor)
        {
            InitializeComponent();
            _panelContenedor = panelContenedor;
            this.Translate();

            var repo = Factory.Current.GetBookingRepository();
            var bllService = new BookingService(repo);
            _bookingSLService = new BookingSLService(bllService);

            var stateRepo = Factory.Current.GetBookingStateRepository();
            var stateBll = new BookingStateService(stateRepo);
            _bookingStateSLService = new BookingStateSLService(stateBll);
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

        private void btnRepSales_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepSales(_panelContenedor));
        }

        private void btnRepCan_Click(object sender, EventArgs e)
        {
            OpenFormChild(new MenuRepCan(_panelContenedor));
        }

        /// <summary>
        /// Traduce los encabezados de las columnas del DataGridView
        /// utilizando el mismo sistema de idiomas del resto de la aplicación.
        /// Usa el Name de la columna como clave en los archivos de idioma.
        /// </summary>
        private void TranslateGridHeaders()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
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
        /// Busca reservas por rango de fechas y las muestra en el DataGridView.
        /// </summary>
        private void btnGenRepBooking_Click(object sender, EventArgs e)
        {
            // Podés hacerlos opcionales si activás ShowCheckBox; por ahora se usan siempre.
            DateTime? dateFrom = dateTimePicker1.Value.Date;
            DateTime? dateTo = dateTimePicker2.Value.Date;

            try
            {
                var bookings = _bookingSLService
                    .GetBookingsByDateRange(dateFrom, dateTo)
                    .ToList();

                var statesLookup = _bookingStateSLService.GetStatesLookup();

                var viewData = bookings.Select(b => new
                {
                    b.NroDocument,
                    Fecha = b.RegistrationBooking.ToShortDateString(),
                    HoraInicio = new DateTime(b.StartTime.Ticks).ToString("hh\\:mm"),
                    HoraFin = new DateTime(b.EndTime.Ticks).ToString("hh\\:mm"),
                    Estado = statesLookup.TryGetValue(b.State, out var desc)
                        ? $"{b.State} - {desc}"
                        : b.State.ToString(),
                    Importe = $"${b.ImporteBooking:N2}"
                }).ToList();

                dataGridView1.DataSource = viewData;
                TranslateGridHeaders();

                MessageBox.Show(
                    bookings.Any()
                        ? $"Se encontraron {bookings.Count} reservas."
                        : "No se encontraron reservas en ese rango.",
                    "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al generar reporte de reservas: {ex}", EventLevel.Critical);
                MessageBox.Show("Ocurrió un error al buscar reservas.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un PDF con las reservas listadas actualmente.
        /// </summary>
        private void btnPrintBookings_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos PDF (*.pdf)|*.pdf",
                    FileName = $"Reporte_Reservas_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    var doc = new Document(PageSize.A4, 40, 40, 40, 40);
                    PdfWriter.GetInstance(doc, stream);
                    doc.Open();

                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    var footerFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9);

                    doc.Add(new Paragraph("Reporte de Reservas – " + DateTime.Now.ToShortDateString(), titleFont));
                    doc.Add(new Paragraph(" "));

                    var table = new PdfPTable(
                        dataGridView1.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible))
                    {
                        WidthPercentage = 100
                    };

                    // Encabezados
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (!column.Visible) continue;

                        table.AddCell(new PdfPCell(new Phrase(column.HeaderText, headerFont))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY
                        });
                    }

                    decimal total = 0;

                    // Filas
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (!cell.OwningColumn.Visible) continue;

                            var value = cell.Value?.ToString() ?? string.Empty;
                            table.AddCell(new Phrase(value, normalFont));

                            if (cell.OwningColumn.HeaderText.ToLower().Contains("importe"))
                            {
                                var raw = value.Replace("$", "").Trim();

                                if (decimal.TryParse(raw, NumberStyles.Any, new CultureInfo("es-AR"), out decimal val))
                                    total += val;
                            }
                        }
                    }

                    doc.Add(table);
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Total de registros: {dataGridView1.Rows.Count}", normalFont));
                    doc.Add(new Paragraph($"Total general: ${total:N2}", normalFont));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("Generado automáticamente por el sistema SIGACF.", footerFont));

                    doc.Close();
                }

                MessageBox.Show("PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
