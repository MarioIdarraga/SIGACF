using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SL.Domain.BusinessException;

namespace SL.DAL
{

    public sealed class LanguageDAL
    {

        #region Singleton
        private readonly static LanguageDAL _instance = new LanguageDAL();

        public static LanguageDAL Current
        {
            get
            {
                return _instance;
            }
        }

        private LanguageDAL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

		private string folderLanguage = System.Configuration.ConfigurationManager.AppSettings["FolderLanguage"];
        private string filePathLanguage = System.Configuration.ConfigurationManager.AppSettings["FilePathLanguage"];
        public string Traductor(string key)
        {
            string filePath = $"{folderLanguage}/{filePathLanguage}{Thread.CurrentThread.CurrentUICulture.Name}";

            
            if (!Directory.Exists(folderLanguage))
                Directory.CreateDirectory(folderLanguage);

            if (!File.Exists(filePath))
                File.Create(filePath).Close(); 

            var lines = File.ReadAllLines(filePath).ToList();

            foreach (var line in lines)
            {
                string[] dataFile = line.Split('=');
                if (dataFile.Length == 2 && dataFile[0].Trim() == key)
                {
                    return dataFile[1].Trim();
                }
            }

            string newEntry = $"{key}={key}";
            File.AppendAllLines(filePath, new[] { newEntry });

            return key;
        }


        public List<string> GetAllLanguage()
        {
            List<string> languages = new List<string>();
            string filePath = $"{folderLanguage}/{filePathLanguage}";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string[] dataFile = sr.ReadLine().Split('=');
                    languages.Add(dataFile[0]);
                }
            }
            return languages;
        }


    }
}