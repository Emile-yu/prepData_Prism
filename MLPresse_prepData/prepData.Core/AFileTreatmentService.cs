using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public abstract class AFileTreatmentService : IFileTreatmentService
    {
        public virtual string InputFileName { get; protected set; }

        public virtual string OutputFileName { get; protected set; }

        public virtual string OutputPathName { get; protected set; }

        //#region constructor
        //public AFileTreatmentService(BackgroundWorker Worker)
        //{
        //    _Worker = Worker;
        //}

        //public AFileTreatmentService(String fileName, BackgroundWorker Worker)
        //{
        //    this._fileName = fileName;
        //    _Worker = Worker;
        //}
        //#endregion

        #region abstract members

        public abstract void ImportFile();

        public abstract DataLogs ExportFile();

        #endregion
    }
}
