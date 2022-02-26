using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace prepData_Business.Configuration
{
    public class ManagerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("modules")]
        public ModulesElementCollection Modules
        {
            get
            {
                return (ModulesElementCollection)base["modules"];
            }
        }
    }

    public class ModulesElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleElement)element).Name; 
        }

        public ModuleElement GetModuleByName(string name)
        {
            return (ModuleElement)this.BaseGet(name);
        }
    }

    public class ModuleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
        }
    }
}
