using Avalonia.Controls;
using Avalonia.Threading;
using MinecraftLaunch.Modules.Analyzers;
using MinecraftLaunch.Modules.Models.Launch;
using Natsurainko.FluentCore.Class.Model.Launch;
using Natsurainko.FluentCore.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCSMCL.Modules.Base;
using WCSMCL.Modules.Models;
using WCSMCL.Modules.Toolkits;

namespace WCSMCL.ViewModels
{
    public partial class ConsoleWindowViewModel : ViewModelBase
    {
        public double AWidth
        {
            get => _T;
            set => RaiseAndSetIfChanged(ref _T, value);
        }

        public ObservableCollection<LogModels> Outputs
        {
            get => _Logs;
            set => RaiseAndSetIfChanged(ref _Logs, value);
        }

        public LogModels LastOutput
        {
            get => _Log;
            set => RaiseAndSetIfChanged(ref _Log, value);
        }
    }

    partial class ConsoleWindowViewModel
    {
        public ConsoleWindowViewModel(List<string> outputs, JavaClientLaunchResponse process, ScrollViewer box) {        
            ShowLogTypeBar();
            Process = process;
            Box = box;
            if (outputs is not null && outputs.Count > 1) {
                outputs.AsParallel().ToList().ForEach(x =>
                {
                    var output = GameLogAnalyzer.AnalyseAsync(x);
                    Outputs.Add(output.ToOutput());
                });

                LastOutput = Outputs.Last();
            }

            Process.ProcessOutput += Process_ProcessOutput;
        }

        private async void Process_ProcessOutput(object? sender, MinecraftLaunch.Modules.Interface.IProcessOutput e)
        {
            Outputs.Add(GameLogAnalyzer.AnalyseAsync(e.Raw).ToOutput());
            LastOutput = Outputs.Last();
            
            Trace.WriteLine($"[����] Outputs������Ϊ {LastOutput.Log}");
            await Dispatcher.UIThread.InvokeAsync(() => Box.ScrollToEnd());
        }

        public async void ShowLogTypeBar()
        {
            await Task.Run(async() =>
            {
                AWidth = 0;
                await Task.Delay(400);
                AWidth = 35;
            });
        }

        public void KillGame()
        {
            if (!Process.Process.HasExited)
                Process.Process.Kill();
        }
    }

    partial class ConsoleWindowViewModel
    {
        ScrollViewer Box;
        public JavaClientLaunchResponse Process = null;
        public Process GameProcess = null;
        public ConsoleWindowViewModel() => ShowLogTypeBar();
        public double _T = 0;
        public ObservableCollection<LogModels> _Logs = new();
        public LogModels _Log = new();
    }
}
