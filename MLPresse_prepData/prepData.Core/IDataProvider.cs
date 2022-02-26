using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public interface IDataProvider<T>
    {
        string _filePath { get; }

        int _posStart { get; }

        int _posEnd { get; }
        List<T> Provider();
    }
}
