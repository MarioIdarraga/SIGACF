using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SL.BLL;

namespace UI.Helpers
{
    public static class TranslatorHelper
    {
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
                    c.Translate(); // recursivo
            }
        }

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
            // También en carpeta del proyecto (para revisión)
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
