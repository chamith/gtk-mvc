using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Gtk.Mvc.Configuration
{
    public class MvcConfiguration:ConfigurationSection
    {

        [ConfigurationProperty("assemblies")]
        public AssemblyConfigurationCollection Assemblies
        {
            get
            {
                return (AssemblyConfigurationCollection)this["assemblies"];
            }
            set
            {
                this["assemblies"] = value;
            }
        }

    }
}
