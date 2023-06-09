using Avalonia.Controls;
using Avalonia.Threading;
using System.Collections.Generic;
using WCSMCL.Modules.Base;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Models;

namespace WCSMCL.Views
{
    public partial class ConsoleView : Page
    {
        public static List<LogModels> logModels = new();
        public static ConsoleView console;
        public ConsoleView()
        {
            InitializeComponent();
            console = this;
            loglist = this.Find<ListBox>("loglist");
            TaskBase.InvokeAsync(() =>
            {
                if (logModels is not null)
                    loglist.Items = logModels;
                loglist.ScrollIntoView(loglist.ItemCount - 1);
            });
        }

        public void AddLog(LogModels l)
        {
            TaskBase.InvokeAsync(() =>
            {
                ConsoleView.console.loglist.Items = null;
                logModels.Add(l);
                if (console != null)
                    loglist.Items = logModels;
                loglist.ScrollIntoView(loglist.ItemCount - 1);
            });
        }
    }
}
