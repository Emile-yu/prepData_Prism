using CsvHelper.Configuration;
using prepData.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Support.Models
{
    public class Supports : ILineParser, ISerializable
    {
        public int MedNum { get; set; }

        public int IdTitre { get; set; }

        public int Parution { get; set; }

        public int Jour { get; set; }

        public bool Parse(string line, int posStart = 0, int posEnd = 0)
        {
            return true;
        }

        public string Serialize()
        {
            return $"{MedNum};{IdTitre};{Parution};{Jour}";
        }
    }

    public class SupportMapClass : ClassMap<Supports>
    {
        public SupportMapClass()
        {
            Map(m => m.MedNum).Name("med_num0");
            Map(m => m.IdTitre).Name("ID_ONE_titre");
            Map(m => m.Parution).Name("ID_parution");
            Map(m => m.Jour).Name("Rang_jour");
        }
    }

    public class SupportExport
    {
        public int MedNum { get; set; }

        public int Parution { get; set; }

        public int Jour { get; set; }

        public SupportExport(int medNum, int parution, int jour)
        {
            this.MedNum = medNum;
            this.Parution = parution;
            this.Jour = jour;
        }
    }
}
