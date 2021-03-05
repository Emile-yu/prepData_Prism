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
        protected String _fileName = "";

        protected String _OutputFileName = "";

        protected String _OutputPathName = "";

        protected BackgroundWorker _Worker;
        #region constructor
        public AFileTreatmentService(BackgroundWorker Worker, String fileName="")
        {
            _Worker = Worker;
        }

        public AFileTreatmentService(String fileName, BackgroundWorker Worker)
        {
            this._fileName = fileName;
            _Worker = Worker;
        }
        #endregion

        #region abstract members

        public abstract void ImportFile();

        public abstract void ExportFile();

        #endregion
    }
}
