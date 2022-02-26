using prepData.Core;
using prepData.Individual.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Individual.Business
{
    public class IndividuManager : DataProvider<Individu>, IExport<Individu>
    {
        public IndividuManager(string filePath, int posStart, int posEnd) : base(filePath, posStart, posEnd)
        {
        }

        public void Export(string fileName, List<Individu> Individus)
        {
           
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var ind in Individus)
                {
                    writer.WriteLine(ind.Serialize());
                }
            }
        }
    }
}
