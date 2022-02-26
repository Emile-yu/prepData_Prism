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
    public class SupportTreatmentService : AFileTreatmentServiceSupport
    {
        #region private propoty

        private SupportManager _supportManager;

        #endregion

        #region public propoty
 
        public Dictionary<int, List<Supports>> Supports { get; private set; }

        #endregion

        //#region constructor
        //public SupportTreatmentService(String fileName, BackgroundWorker Worker) : base(fileName, Worker)
        //{
        //    Supports = new Dictionary<int, List<Supports>>();
        //    ImportFile();
        //}
        //#endregion

        #region operations
        public override void Initialize(string fileName)
        {
            this.InputFileName = fileName;
            ImportFile();
        }
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

        public override DataLogs ExportFile()
        {

            if (Supports == null && !Supports.Any())
            {
                //Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("Erreur de données, veuillez vérifier")));
                return new DataLogs(LogType.Error, String.Format("Erreur de données, veuillez vérifier"));
            }

            this.OutputPathName = FilePathManager.getInstance().getPathName(DataType.Support, this.InputFileName);
            this.OutputFileName = FilePathManager.getInstance().getFileName(DataType.Support, this.InputFileName);

            if (!Directory.Exists(this.OutputPathName))
            {
                Directory.CreateDirectory(this.OutputPathName);
            }

            try
            {

                foreach (KeyValuePair<int, List<Supports>> item in Supports)
                {
                    //Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("{0}.csv est en cours de générer ...", this.OutputFileName + item.Key)));

                    _supportManager.Export(this.OutputFileName + item.Key + ".csv", item.Value);
                    //WriteBinary(this.OutputFileName + item.Key + ".bin", item.Value);

                    //Worker.ReportProgress(1, new DataLogs(LogType.Success, String.Format("{0}.csv est généré ...", this.OutputFileName + item.Key)));
                }
                return new DataLogs(LogType.Success, String.Format("tous les fichiers du support ont été générés ..."));
            }
            catch (Exception e)
            {
                return new DataLogs(LogType.Error, e.Message);
            }

            

        }

        private void WriteBinary(string fileName, List<Supports> datas)
        {
            using (var writer = new FileStream(fileName, FileMode.CreateNew))
            {
                BinaryWriter bw = new BinaryWriter(writer);
                List<SupportExport> result = datas.Select(o => new SupportExport(Tools.ParseIdOfIndividual(o.MedNum), o.IdTitre, o.Parution, o.Jour)).ToList();
                foreach (SupportExport data in result)
                {
                    bw.Write(data.MedNum);
                    bw.Write(data.IdTitre);
                    bw.Write(data.Parution);
                    bw.Write(data.Jour);
                }
            }
                
        }

        #endregion
    }
}
