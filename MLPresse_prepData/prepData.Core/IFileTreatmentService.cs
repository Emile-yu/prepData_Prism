using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public interface IFileTreatmentService
    {

        //string InputFileName { get;}

        //string OutputFileName { get; }

        //string OutputPathName { get; }

        //BackgroundWorker Worker { get;}

        void ImportFile();

        void ExportFile();
    }
}
