using prepData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Individual.Models
{
    public class Individu : ILineParser, ISerializable
    {
        public string Id { get; set; }

        public bool Parse(string line, int posStart, int posEnd)
        {
            Id = Tools.ParseIdOfIndividual(line.Substring(posStart, posEnd - posStart).Trim());
            return true;
        }

        public string Serialize()
        {
            return $"{Id}";
        }
    }
}
