using prepData.Core;
using prepData.Individual.Business;
using prepData.Individual.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Individual.Service
{
    public class IndividuFileTreatment : AFileTreatmentServiceIndividual
    {
        #region private

        private IndividuManager _individuManager;

        #endregion

        #region membre private
        public int StartPos { get; private set; }

        public int EndPos { get; private set; }

        public List<Individu> Individus { get; private set; }
        #endregion

        //#region constructor
        //public IndividuFileTreatment(String fileName, BackgroundWorker Worker, int Start, int End) : base(fileName, Worker)
        //{
        //    StartPos = Start;
        //    EndPos = End;

        //    ImportFile();
        //}
        //#endregion

        #region operations
        public override void Initialize(string fileName, int start, int end)
        {
            this.InputFileName = fileName;
            StartPos = start;
            EndPos = end;
        }
        public override void ImportFile()
        {
            if (String.IsNullOrEmpty(this.InputFileName))
            {
                throw new Exception("File Name is vide");
            }

            if (this.EndPos < 0 || this.StartPos < 0)
            {
                throw new Exception("The position can not be negative !");
            }

            if (this.EndPos < this.StartPos)
            {
                throw new Exception("The starting position is wrong !");
            }

            //_Worker.ReportProgress(1, new DataLogs(LogType.None, "traitement en cours..."));

            _individuManager = new IndividuManager(this.InputFileName, StartPos, EndPos);

            Individus = _individuManager.Provider();

            //Individus = File.ReadAllLines(this._fileName, Encoding.GetEncoding("iso-8859-15")).Select(l => l.Substring(this.StartPos, this.EndPos - this.StartPos)).ToList();


        }
        public override DataLogs ExportFile()
        {
            if (Individus == null && !Individus.Any())
            {
                //Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("Erreur de données, veuillez vérifier")));
                return new DataLogs(LogType.Error, String.Format("Erreur de données, veuillez vérifier"));
            }

            this.OutputPathName = FilePathManager.getInstance().getPathName(DataType.Individu, this.InputFileName);
            this.OutputFileName = OutputPathName + "\\Individus";

            if (!Directory.Exists(this.OutputPathName))
            {
                Directory.CreateDirectory(this.OutputPathName);
            }
            string l_fileName = this.OutputFileName + ".csv";

            try
            {
                //Worker.ReportProgress(1, new DataLogs(LogType.None, String.Format("{0}.csv est en cours de générer ...", l_fileName)));
                _individuManager.Export(l_fileName, Individus);
                //Worker.ReportProgress(1, new DataLogs(LogType.Success, String.Format("{0}.csv est généré ...", l_fileName)));
                return new DataLogs(LogType.Success, String.Format("Le fichier de l'individu est généré ...", l_fileName));
            }
            catch (Exception e)
            {
                return new DataLogs(LogType.Error, e.Message);
            }

        }
    #endregion
}
}
