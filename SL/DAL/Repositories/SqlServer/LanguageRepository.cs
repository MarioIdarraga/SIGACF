using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Domain.BusinessException;

namespace SL.DAL.Repositories.SqlServer
{


	public sealed class LanguajeRepository

	{
		#region Singleton
		private readonly static LanguajeRepository _instance = new LanguajeRepository();

		public static LanguajeRepository Current
		{
			get
			{
				return _instance;
			}
		}

		private LanguajeRepository()
		{
			//Implent here the initialization of your singleton
		}

		#endregion


		private string folderLanguage = ConfigurationManager.AppSettings["FolderLanguage"];

        private string filePathLanguage = ConfigurationManager.AppSettings["FilePathLanguage"];
        public string Traductor(string key, string lang)
		{
			string filePath = $"{folderLanguage}/{filePathLanguage}{lang}";

			using (StreamReader sr = new StreamReader(filePath))
			{
				while (!sr.EndOfStream)
				{
					string[] dataFile = sr.ReadLine().Split('=');

					if (dataFile[0] == key);
					{
						return dataFile[1];
                    }
                }

			}

			throw new NoSeEncontroLaPalabraException();
        }

		public List<string> GetAllLanguage()
        {
            List<string> languages = new List<string>();
            string[] files = Directory.GetFiles(folderLanguage, "*.txt");
            foreach (string file in files)
            {
                languages.Add(Path.GetFileNameWithoutExtension(file));
            }
            return languages;
        }

    }
}