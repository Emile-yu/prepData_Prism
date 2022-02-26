using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public abstract class AFileTreatmentServiceRi : AFileTreatmentService
    {
        public abstract void Initialize(string descrFilePath, string dataFilePath);
    }
}
