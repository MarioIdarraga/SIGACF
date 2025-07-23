using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SL.Services;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-AR");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //// ✅ Validación de integridad antes del login
            //var verificadorService = new DVVService();
            //bool sistemaIntegro = verificadorService.VerificarIntegridadCompleta();

            //if (!sistemaIntegro)
            //{
            //    MessageBox.Show("Error de integridad detectado. Contacte al administrador.", "Integridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return; // ⛔ No permitir avanzar
            //}

            Application.Run(new Login());
        }
    }
}


