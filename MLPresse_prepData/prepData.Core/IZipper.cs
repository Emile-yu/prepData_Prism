using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public interface IZipper
    {
        void Zipper(string path);
    }
}
