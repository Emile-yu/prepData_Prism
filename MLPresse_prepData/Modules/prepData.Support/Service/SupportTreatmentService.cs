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
 
        public Dictionary<int, List<Supports>> Supports { get; private set; }

        #endregion

        #region constructor
        public SupportTreatmentService(String fileName, BackgroundWorker Worker) : base(fileName, Worker)
        {
            Supports = new Dictionary<int, List<Supports>>();
            ImportFile();
        }
        #endregion

        #region operations
        public override void ImportFile()
        {
            if (String.IsNullOrEmpty(this._fileName))
            {
                throw new Exception("File name is empty");
            }

            //_Worker.ReportProgress(1, new DataLogs(LogType.None, "traitement en cours..."));

            _supportManager = new SupportManager(_fileName);

            Supports = _supportManager.Provider().GroupBy(l => l.IdTitre).ToDictionary(o => o.Key, o => o.ToList());
        }

        public override void ExportFile()
        {

            if (Supports == null && !Supports.Any())
            {
                _Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("Erreur de données, veuillez vérifier")));
                return;
            }

            this._OutputPathName = FilePathManager.getInstance().getPathName(DataType.Support, this._fileName);
            this._OutputFileName = FilePathManager.getInstance().getFileName(DataType.Support, this._fileName);


            if (!Directory.Exists(this._OutputPathName))
            {
                Directory.CreateDirectory(this._OutputPathName);
            }

            foreach (KeyValuePair<int, List<Supports>> item in Supports)           
            {
                _Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("{0}.csv est en cours de générer ...", this._OutputFileName + item.Key)));

                _supportManager.Export(this._OutputFileName + item.Key + ".csv", item.Value);

                _Worker.ReportProgress(1, new DataLogs(LogType.Success, String.Format("{0}.csv est généré ...", this._OutputFileName + item.Key)));
            }
        }

        #endregion
    }
}
