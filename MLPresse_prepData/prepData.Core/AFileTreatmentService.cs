using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public abstract class AFileTreatmentService : IFileTreatmentService
    {
        //public virtual string InputFileName { get; protected set; }

        //public virtual string OutputFileName { get; protected set; }

        //public virtual string OutputPathName { get; protected set; }

        //public virtual BackgroundWorker Worker { get; protected set; }

        protected string InputFileName = "";

        protected string OutputFileName = "";

        protected string OutputPathName = "";

        protected BackgroundWorker Worker { get; set; }

        #region constructor
        public AFileTreatmentService(BackgroundWorker worker, String fileName = "")
        {
            Worker = worker;
        }

        public AFileTreatmentService(String fileName, BackgroundWorker worker)
        {
            this.InputFileName = fileName;
            Worker = worker;
        }
        #endregion

        #region abstract members

        public abstract void ImportFile();

        public abstract void ExportFile();

        #endregion
    }
}
