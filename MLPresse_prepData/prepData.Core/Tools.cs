using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public static class Tools
    {
        private static int limit = 9; 
        public static string ParseIdOfIndividual(string id) 
        {
            Int64 idInt = Int64.Parse(id);
            id = idInt.ToString();
            string res = id;
            int len = id.Length;
            if (len > limit)
                res = id.Substring(0, limit);
            return res;
        }
    }
}
