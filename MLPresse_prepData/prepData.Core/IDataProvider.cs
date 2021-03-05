using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public interface IDataProvider<T> where T : ILineParser
    {
        List<T> Provider();
    }
}
