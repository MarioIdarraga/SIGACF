using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SL.BLL;

namespace UI.Helpers
{
    /// <summary>
    /// Proporciona métodos de traducción para controles de interfaz,
    /// aplicando el idioma configurado y registrando automáticamente
    /// las traducciones faltantes.
    /// </summary>
    public static class TranslatorHelper
    {
        /// <summary>
        /// Traduce de forma recursiva todos los controles contenidos en el control recibido.
        /// Aplica el traductor actual y registra las claves faltantes.
        /// </summary>
        /// <param name="control">Control raíz cuyos textos serán traducidos.</param>
        public static void Translate(this Control control)
        {
            foreach (Control c in control.Controls)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(c.Text))
                    {
                        c.Text = LanguageBLL.Current.Traductor(c.Text);
                    }
                }
                catch
                {
                    AddMissingTranslation(c.Text, c.Text);
                }

                if (c.HasChildren)
                    c.Translate(); 
            }
        }

        /// <summary>
        /// Agrega una traducción faltante al archivo de idioma en tiempo de ejecución,
        /// y también al archivo correspondiente del proyecto cuando está en modo DEBUG.
        /// </summary>
        /// <param name="key">Clave de texto que falta traducir.</param>
        /// <param name="defaultText">Texto por defecto que se guardará.</param>
        private static void AddMissingTranslation(string key, string defaultText)
        {
            string lang = Thread.CurrentThread.CurrentUICulture.Name;
            string fileName = $"Language.{lang}"; // sin .txt

            string runtimeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["FolderLanguage"]);
            string runtimePath = Path.Combine(runtimeFolder, fileName);

            if (!File.Exists(runtimePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(runtimePath));
                File.Create(runtimePath).Close();
            }

            var allLines = File.ReadAllLines(runtimePath);
            if (!allLines.Any(line => line.StartsWith(key + "=")))
            {
                File.AppendAllText(runtimePath, $"{key}={defaultText}\n");
            }

#if DEBUG
            
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\I18n"));
            string projectPath = Path.Combine(projectFolder, fileName);

            if (!File.Exists(projectPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(projectPath));
                File.Create(projectPath).Close();
            }

            var projectLines = File.ReadAllLines(projectPath);
            if (!projectLines.Any(line => line.StartsWith(key + "=")))
            {
                File.AppendAllText(projectPath, $"{key}={defaultText}\n");
            }
#endif
        }
    }
}
