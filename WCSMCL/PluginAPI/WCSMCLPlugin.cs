using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCSMCL.PluginAPI
{
    public abstract class WCSMCLPlugin : PluginLoader.Plugin
    {
        public abstract void OnPluginLoad();
        public abstract void OnPluginUnLoad();
        public abstract void OnSettingButtonClick();
    }
}
