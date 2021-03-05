using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public interface IExport<T>
    {
        void Export(string fileName, List<T> result);
    }
}
