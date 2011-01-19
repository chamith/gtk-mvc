using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Gtk.Mvc.Configuration
{
    public class AssemblyConfigurationCollection:ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AssemblyConfiguration)element).Name;
        }

        public AssemblyConfiguration this[int index]
        {
            get
            {
                return (AssemblyConfiguration)BaseGet(index);
            }
        }

        new public AssemblyConfiguration this[string name]
        {
            get
            {
                return (AssemblyConfiguration)BaseGet(name);
            }
        }

    }
}
