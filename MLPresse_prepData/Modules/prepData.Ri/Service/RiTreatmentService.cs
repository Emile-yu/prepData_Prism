using prepData.Core;
using prepData.Ri.Models;
using prepData.Ri.Business.DataSource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prepData.Ri.Business;

namespace prepData.Ri.Service
{
    public class RiTreatmentService : AFileTreatmentServiceRi
    {
        #region private membres

        private RiManager _riManager;

        private RiDescrFileManager _riDescrFileManager;

        public List<RiDescrFile> riDescrFiles { get; set; }

        #endregion

        #region public properties

        public String DescrFilePath { get; private set; }

        public String DataFilePath { get; private set; }

        public List<Ris> SupportRis { get; private set; }

        #endregion

        //#region conctructor
        //public RiTreatmentService(String descrFilePath, String dataFilePath, BackgroundWorker Worker) : base(Worker)
        //{
        //    DescrFilePath = descrFilePath;

        //    DataFilePath = dataFilePath;

        //    ImportFile();

        //}
        //#endregion

        #region operations
        public override void Initialize(string descrFilePath, string dataFilePath)
        {
            DescrFilePath = descrFilePath;
            DataFilePath = dataFilePath;
            ImportFile();
        }
        public override void ImportFile()
        {
            //_Worker.ReportProgress(1, new DataLogs(LogType.None, "traitement en cours..."));
            if (String.IsNullOrEmpty(DescrFilePath))
                throw new Exception("Descr File's name is empty");

            if (String.IsNullOrEmpty(DataFilePath))
                throw new Exception("Data file's name is empty");

            _riDescrFileManager = new RiDescrFileManager(DescrFilePath);
            riDescrFiles = _riDescrFileManager.Provider();

            _riManager = new RiManager(DataFilePath, riDescrFiles);
            SupportRis = _riManager.Provider();
        }
        public override DataLogs ExportFile()
        {
            if (SupportRis == null && !SupportRis.Any())
            { 
                //Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("Erreur de données, veuillez vérifier")));
                return new DataLogs(LogType.Error, String.Format("Erreur de données, veuillez vérifier"));
            }

            this.OutputPathName = FilePathManager.getInstance().getPathName(DataType.Ri, this.DescrFilePath);
            this.OutputFileName = FilePathManager.getInstance().getFileName(DataType.Ri, this.DescrFilePath);

            if (!Directory.Exists(this.OutputPathName))
            {
                Directory.CreateDirectory(this.OutputPathName);
            }

            try
            {
                foreach (var sr in SupportRis)
                {
                    foreach (var item in sr.GetRis())
                    {
                        //Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("{0}.csv est en cours de générer ...", this.OutputFileName + item.Key)));

                        _riManager.Export(OutputFileName + item.Key + ".csv", item.Value.GetRiIndividu());

                        //Worker.ReportProgress(1, new DataLogs(LogType.Success, String.Format("{0}.csv est généré ...", OutputFileName + item.Key)));
                    }
                }
                return new DataLogs(LogType.Success, String.Format("tous les fichiers du ri ont été générés ..."));
            }
            catch (Exception e)
            {
                return new DataLogs(LogType.Error, e.Message);
            }
        }
        #endregion
    }
}
