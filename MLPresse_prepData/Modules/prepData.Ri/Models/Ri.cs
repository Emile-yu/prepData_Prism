using CsvHelper.Configuration;
using prepData.Core;
using prepData.Ri.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Ri.Models
{
    public class Ris : ILineParser
    {
        /// <summary>
        /// un support avec une liste d'individu
        /// </summary>
        private Dictionary<int/*supportId*/, RiIndividu> _ris;

        public Ris()
        {
            _ris = new Dictionary<int, RiIndividu>();
        }

        public void Add(int suppId, Int64 indIndividuId, double ri)
        {
            if (!_ris.ContainsKey(suppId))
                _ris.Add(suppId, new RiIndividu(suppId, indIndividuId, ri));
            else
                _ris[suppId].Add(suppId,indIndividuId, ri);
        }

        public Dictionary<int, RiIndividu> GetRis()
        {
            return _ris;
        }

        public bool Parse(string line, int posStart = 0, int posEnd = 0)
        {
            return true;
        }
    }

    public class RiDescrFile : ILineParser
    {
        public int PosBegin { get; set; }

        public int PosEnd { get; set; }

        public string Type { get; set; }

        public string SupportCode { get; set; }

        //public string Nb { get; set; }

        public bool Parse(string line, int posStart = 0, int posEnd = 0)
        {
            return true;
        }

        #region operations
        public bool IsIndividual()
        {
            return Type == "Individual";
        }

        public bool IsSupport()
        {
            return Type == "Support";
        }
        #endregion
    }

    public class RiDescrFileClassMap : ClassMap<RiDescrFile>
    {
        public RiDescrFileClassMap()
        {
            Map(m => m.PosBegin).Name("POS1");
            Map(m => m.PosEnd).Name("POS2");
            Map(m => m.Type).ConvertUsing(row => row.GetField<string>(3) == "ID" ?
            "Individual" : "Support"
            );
            Map(m => m.SupportCode).Name("CODE");//.ToString().Substring(1);
            //Map(m => m.Nb).Name("NB #0");
        }
    }
}
