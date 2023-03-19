using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WCSMCL.Modules.Controls;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class GameSettingView : Page
    {
        public GameSettingViewModel ViewModel = new();
        public GameSettingView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        public override void OnNavigatedTo()
        {
            try {
                if (App.Data.GameFooterList is not null && App.Data.GameFooterList.Count != 0) {
                    ViewModel.CurrentGameFolder = App.Data.FooterPath;
                }

                ViewModel.IsOlate = App.Data.Isolate;
            } catch (Exception ex) {
                Trace.WriteLine("[Error] " + ex.ToString());
            }
        }

        private void InitializeComponent() => InitializeComponent(true);
    }
}
