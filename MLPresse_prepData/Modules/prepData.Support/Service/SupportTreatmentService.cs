using prepData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using prepData.Support.Models;
using prepData.Support.Business;

namespace prepData.Support.Service
{
    public class SupportTreatmentService : AFileTreatmentService
    {
        #region private propoty

        private SupportManager _supportManager;

        #endregion

        #region public propoty

        public Dictionary<int, List<Supports>> Supports { get; private set; } = new Dictionary<int, List<Supports>>();

        #endregion

        #region constructor
        public SupportTreatmentService(String fileName, BackgroundWorker Worker) : base(fileName, Worker)
        {
            ImportFile();
        }
        #endregion

        #region operations
        //public void Initialize(string fileName, BackgroundWorker worker)
        //{
        //    this.InputFileName = fileName;
        //    this.Worker = worker;

        //    ImportFile();
        //}

        public override void ImportFile()
        {
            if (String.IsNullOrEmpty(this.InputFileName))
            {
                throw new Exception("File name is empty");
            }

            //_Worker.ReportProgress(1, new DataLogs(LogType.None, "traitement en cours..."));

            _supportManager = new SupportManager(InputFileName);

            Supports = _supportManager.Provider().GroupBy(l => l.IdTitre).ToDictionary(o => o.Key, o => o.ToList());
        }

        public override void ExportFile()
        {

            if (Supports == null && !Supports.Any())
            {
                Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("Erreur de données, veuillez vérifier")));
                return;
            }

            this.OutputPathName = FilePathManager.getInstance().getPathName(DataType.Support, this.InputFileName);
            this.OutputFileName = FilePathManager.getInstance().getFileName(DataType.Support, this.InputFileName);


            if (!Directory.Exists(this.OutputPathName))
            {
                Directory.CreateDirectory(this.OutputPathName);
            }

            foreach (KeyValuePair<int, List<Supports>> item in Supports)           
            {
                Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("{0}.csv est en cours de générer ...", this.OutputFileName + item.Key)));

                _supportManager.Export(this.OutputFileName + item.Key + ".csv", item.Value);

                Worker.ReportProgress(1, new DataLogs(LogType.Success, String.Format("{0}.csv est généré ...", this.OutputFileName + item.Key)));
            }
        }

        #endregion
    }
}
