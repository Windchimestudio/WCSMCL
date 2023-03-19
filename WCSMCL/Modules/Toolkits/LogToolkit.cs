using Avalonia.Controls;
using MinecraftLaunch.Modules.Toolkits;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCSMCL.Modules.Enum;

namespace WCSMCL.Modules.Toolkits
{
    public class LogToolkit
    {
        public static void WriteLine<T>(T raw,LogTyoe tyoe = LogTyoe.Info)
        {
            Debug.WriteLine($"[{TimeToolkit.GetCurrentTimeSlot()}] [{tyoe}] {raw}");
        }
    }
}