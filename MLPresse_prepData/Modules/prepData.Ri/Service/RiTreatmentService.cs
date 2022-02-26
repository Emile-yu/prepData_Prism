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
    public class RiTreatmentService : AFileTreatmentService
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

        #region conctructor
        public RiTreatmentService(String descrFilePath, String dataFilePath, BackgroundWorker Worker) : base(Worker)
        {
            DescrFilePath = descrFilePath;

            DataFilePath = dataFilePath;

            ImportFile();

        }
        #endregion

        #region operations
        //public void Initialize(string descrFilePath, string dataFilePath, BackgroundWorker worker)
        //{
        //    DescrFilePath = descrFilePath;
        //    DataFilePath = dataFilePath;
        //    this.Worker = worker;
        //    ImportFile();
        //}
        public override void ImportFile()
        {
            //_Worker.ReportProgress(1, new DataLogs(LogType.None, "traitement en cours..."));

            _riDescrFileManager = new RiDescrFileManager(DescrFilePath);
            riDescrFiles = _riDescrFileManager.Provider();

            _riManager = new RiManager(DataFilePath, riDescrFiles);
            SupportRis = _riManager.Provider();
        }
        public override void ExportFile()
        {
            if (SupportRis == null && !SupportRis.Any())
            { 
                Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("Erreur de données, veuillez vérifier")));
                return;
            }

            this.OutputPathName = FilePathManager.getInstance().getPathName(DataType.Ri, this.DescrFilePath);
            this.OutputFileName = FilePathManager.getInstance().getFileName(DataType.Ri, this.DescrFilePath);

            if (!Directory.Exists(this.OutputPathName))
            {
                Directory.CreateDirectory(this.OutputPathName);
            }
            foreach (var sr in SupportRis)
            {
                foreach (var item in sr.GetRis())
                {
                    Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("{0}.csv est en cours de générer ...", this.OutputFileName + item.Key)));

                    _riManager.Export(OutputFileName + item.Key + ".csv", item.Value.GetRiIndividu());

                    Worker.ReportProgress(1, new DataLogs(LogType.Success, String.Format("{0}.csv est généré ...", OutputFileName + item.Key)));
                }
            }
        }
        #endregion
    }
}
