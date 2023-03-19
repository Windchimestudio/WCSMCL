using Avalonia.Threading;
using MinecraftLaunch.Modules.Models.Download;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Toolkits;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WCSMCL.Modules.Base;
using WCSMCL.Modules.Const;
using WCSMCL.Modules.Models;
using WCSMCL.Modules.Toolkits;

namespace WCSMCL.ViewModels
{
    public partial class ModPropertyViewModel : ViewModelBase
    {
        public List<ModDataModel> ModPacks
        {
            get => _ModPacks;
            set => RaiseAndSetIfChanged(ref _ModPacks, value);
        }

        public bool HasMod
        {
            get => _HasMod;
            set => RaiseAndSetIfChanged(ref _HasMod, value);
        }

        public bool Isolate
        {
            get => _Isolate;
            set => RaiseAndSetIfChanged(ref _Isolate, value);
        }
    }

    partial class ModPropertyViewModel
    {
        public async void LoadModList()
        {
            List<ModDataModel> temp = new();
            ModPacks = null;
            var orgin = (await Toolkit.LoadAllAsync()).ToList();
            foreach (var mod in orgin.AsParallel())
                temp.Add(new(mod));

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ModPacks = temp;
                if (ModPacks.Count > 0)
                    HasMod = false;
                else HasMod = true;
            });
        }
    }

    partial class ModPropertyViewModel
    {
        public static GameCore SelectedGameCore { get; set; }
        public List<ModDataModel> _ModPacks;
        public bool _HasMod = false;
        public bool _Isolate = false;
        public ModPackToolkit Toolkit;
    }
}
