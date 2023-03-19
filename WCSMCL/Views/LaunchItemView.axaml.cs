using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.OpenGL;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media.Animation;
using MinecraftLaunch.Events;
using MinecraftLaunch.Launch;
using MinecraftLaunch.Modules.Analyzers;
using MinecraftLaunch.Modules.Interface;
using MinecraftLaunch.Modules.Models.Auth;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Toolkits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WCSMCL.Modules.Base;
using WCSMCL.Modules.Const;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Interface;
using WCSMCL.Modules.Models;
using WCSMCL.Modules.Toolkits;
using WCSMCL.ViewModels;
using Button = Avalonia.Controls.Button;

namespace WCSMCL.Views
{
    public partial class LaunchItemView : Page, ITask
    {
        public static LaunchItemViewModel ViewModel { get; set; }
        Process GameProcess = null;

        ConsoleWindow Window = null;

        string Path = "";

        public LaunchItemView() => InitializeComponent();

        public LaunchItemView(string version, UserDataModels userData, string javapath)
        {
            MainView.ViewModel.AllTaskCount++;
            InitializeComponent(version, userData, javapath);
        }

        private void InitializeComponent(string version, UserDataModels userData,string javapath)
        {
            InitializeComponent(true);
            ViewModel = new(GameCoreToolkit.GetGameCore(App.Data.FooterPath, version), userData.ToAccount(), javapath);
            DataContext = ViewModel;
            ViewModel.GameLaunchAction();
        }

        public void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TaskView.Remove(this);
            MainView.ViewModel.AllTaskCount--;
        }
    }
}
