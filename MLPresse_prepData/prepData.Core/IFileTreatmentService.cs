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
        string InputFileName { get; }

        string OutputFileName { get; }

        string OutputPathName { get; }
        void ImportFile();

        DataLogs ExportFile();
    }
}
