using CsvHelper;
using prepData.Core;
using prepData.Ri.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Ri.Business.DataSource
{
    public class RiDescrFileManager : DataProvider<RiDescrFile>, IExport<RiDescrFile>
    {
        #region static membres

        private static CsvHelper<RiDescrFileClassMap> s_csvHelper = new CsvHelper<RiDescrFileClassMap>();

        #endregion

        public RiDescrFileManager(string filePath) : base(filePath)
        {
        }
        protected override List<RiDescrFile> GetAllLines(StreamReader reader, int posStart, int posEnd)
        {
            List<RiDescrFile> l_elements;
            l_elements = s_csvHelper.Reader<RiDescrFile>(reader);
            return l_elements;
        }
        public void Export(string fileName, List<RiDescrFile> result)
        {
        }
    }
}
