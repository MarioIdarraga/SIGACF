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
                    c.Translate(); // recursivo para nested controls
            }
        }

        private static void AddMissingTranslation(string key, string defaultText)
        {
            string folder = System.Configuration.ConfigurationManager.AppSettings["FolderLanguage"];
            string file = System.Configuration.ConfigurationManager.AppSettings["FilePathLanguage"];
            string path = Path.Combine(folder, file + Thread.CurrentThread.CurrentUICulture.Name);

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(folder);
                File.Create(path).Close();
            }

            var allLines = File.ReadAllLines(path);
            if (!allLines.Any(line => line.StartsWith(key + "=")))
            {
                File.AppendAllText(path, $"{key}={defaultText}\n");
            }
        }
    }
}

