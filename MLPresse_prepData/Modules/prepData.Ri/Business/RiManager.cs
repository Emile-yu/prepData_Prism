using prepData.Core;
using prepData.Ri.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Ri.Business
{
    public class RiManager : DataProvider<Ris>, IExport<RiData>
    {
        #region static membres

        private static CsvHelper<RiDescrFileClassMap> s_csvHelper = new CsvHelper<RiDescrFileClassMap>();

        #endregion
        public List<RiDescrFile> riDescrFiles { get; private set; }

        public RiManager(string filePath, List<RiDescrFile> riDescrFiles) : base(filePath)
        {
            this.riDescrFiles = riDescrFiles;
        }

        protected override List<Ris> GetAllLines(StreamReader reader, int posStart, int posEnd)
        {
            Ris l_ris = new Ris();

            string l_line;

            int l_totalSize = this.riDescrFiles.Count;

            while ((l_line = reader.ReadLine()) != null)
            {
                string l_individualId = "";

                for (int i = 0; i < l_totalSize; ++i)
                {
                    RiDescrFile l_riDescrFile = this.riDescrFiles[i];
                    int l_begin = l_riDescrFile.PosBegin;
                    int l_end = l_riDescrFile.PosEnd;

                    string value = l_line.Substring(l_begin - 1, l_end - l_begin + 1);

                    if (l_riDescrFile.IsIndividual())
                        l_individualId = value.Trim();
                    else if (l_riDescrFile.IsSupport())
                    {
                        if (l_individualId != "")
                        {
                            int l_suppId = int.Parse(l_riDescrFile.SupportCode.Substring(1));
                            double l_ri = Double.Parse(value);
                            if (l_ri == 0.0) continue;

                            l_ris.Add(l_suppId, Int64.Parse(l_individualId), l_ri);
                        }
                    }    
                }
            }
            return new List<Ris> { l_ris };
        }

        public void Export(string fileName, List<RiData> result)
        {
            using (var writer = new StreamWriter(fileName))
            {
                s_csvHelper.Writer<RiData>(writer, result);
            }
        }
    }
}
