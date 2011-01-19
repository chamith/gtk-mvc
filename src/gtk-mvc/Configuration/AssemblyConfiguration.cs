using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bizline.MVC.Configuration
{
    public class AssemblyConfiguration : ConfigurationSection
    {

        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

		[ConfigurationProperty("assemblyName")]
        public string AssemblyName
        {
            get
            {
                return (string)this["assemblyName"];
            }
        }

        [ConfigurationProperty("controllerRootNamespace")]
        public string ControllerRootNamespace
        {
            get
            {
                return (string)this["controllerRootNamespace"];
            }
        }

        [ConfigurationProperty("viewRootNamespace")]
        public string ViewRootNamespace
        {
            get
            {
                return (string)this["viewRootNamespace"];
            }
        }
    }
}
