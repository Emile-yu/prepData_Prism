using System;
using System.Collections.Generic;
using System.Text;

namespace prepData_Business
{
    public class ModulesCollections : IModulesCollections
    {
        private List<NavigationItem> _modules = new List<NavigationItem>();

        public List<NavigationItem> Modules
        {
            get
            {
                return _modules;
            }
        }
    }
}
