using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Ri.Business
{
    public class RiData
    {
        public Int64 IndividuId { get; private set; }

        public double Ri { get; private set; }

        public RiData(Int64 individuId, double ri)
        {
            this.IndividuId = individuId;
            this.Ri = ri;
        }
    }
    public class RiIndividu
    {

        private List<RiData> _pRiIndividu = new List<RiData>();

        #region conctructor

        public RiIndividu(Int64 IndividuId, double Ri)
        {
            _pRiIndividu.Add(new RiData(IndividuId, (double)(Ri / 1000000.0)));
        }

        #endregion

        #region operation

        public void Add(Int64 IndividuId, double Ri)
        {
            _pRiIndividu.Add(new RiData(IndividuId, (double)(Ri / 1000000.0)));
        }

        public List<RiData> GetRiIndividu()
        {
            return _pRiIndividu;
        }

        #endregion

    }
}
