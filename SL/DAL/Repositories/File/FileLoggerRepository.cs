using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.DAL.Contracts;
using SL.Domain;
using SL.Service.Extension;

namespace SL.DAL.Repositories.File
{
    internal class FileLoggerRepository : ILogger
    {
        private readonly static FileLoggerRepository _instance = new FileLoggerRepository();

        public static FileLoggerRepository Current
        {
            get
            {
                return _instance;
            }
        }

        private FileLoggerRepository()
        {
            //Implent here the initialization of your singleton
        }

        private string LogFileName = ConfigurationManager.AppSettings["LogFileName"];
        private string LogFile = ConfigurationManager.AppSettings["LogFile"];


        public void Store(string message, EventLevel severity)
        {
            string fileName = LogFileName + "-" + DateTime.Now.ToString("yyyyMMdd");

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                string formattedMessage = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} LEVEL: {severity.ToString()} - MENSAJE: {message}";
                sw.WriteLine(formattedMessage);
            }
        }

        internal void StoreCritical(string message, EventLevel severity)
        {

            string fileName = LogFileName + "-" + DateTime.Now.ToString("yyyyMMdd");

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                string formattedMessage = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} LEVEL: {severity.ToString()} - MENSAJE: {message}";
                sw.WriteLine(formattedMessage);
            }
        }

        internal void StoreWarning(string message, EventLevel severity)
        {
            string fileName = LogFileName + "-" + DateTime.Now.ToString("yyyyMMdd");

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                string formattedMessage = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} LEVEL: {severity.ToString()} - MENSAJE: {message}";
                sw.WriteLine(formattedMessage);
            }
        }

        public List<Log> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
