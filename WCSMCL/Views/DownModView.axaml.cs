using Avalonia.Controls;
using MinecraftLaunch.Modules.Toolkits;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Models;
using WCSMCL.Modules.Toolkits;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class DownModView : Page
    {
        public static DownModViewModel ViewModel = new();
        public DownModView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
        
        public void NavigationToModInfo(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
        }

        public async void InstallClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TasksTooklit.CreateModDownloadTask((CurseForgeModel)((Button)sender).DataContext!, sender as Control);

            //DemoBox.Items = GameCoreToolkit.GetGameCores(App.Data.FooterPath).Where(x => x.HasModLoader);
            //await SelectGameCoreDialog.ShowAsync();
            //var core = GameCoreToolkit.GetGameCore(App.Data.FooterPath, App.Data.SelectedGameCore!);
            //if (!string.IsNullOrEmpty(App.Data.SelectedGameCore) && core is not null && core.HasModLoader == true)               
            //{
            //    TasksTooklit.CreateModDownloadTask((CurseForgeModel)((Button)sender).DataContext!, sender as Control);
            //}
            //else if (string.IsNullOrEmpty(App.Data.SelectedGameCore))
            //{
            //    MainWindow.ShowInfoBarAsync("Debug - ���棺","δѡ��Ϸ����", FluentAvalonia.UI.Controls.InfoBarSeverity.Warning);
            //}
            //else if (core is not null && !core.HasModLoader)
            //{
            //    MainWindow.ShowInfoBarAsync("Debug - ���棺", "ѡ�����Ϸ����δ��װģ�������", FluentAvalonia.UI.Controls.InfoBarSeverity.Warning);
            //}
        }

        public async void SaveInstallClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var mod = ((CurseForgeModel)((Button)sender!).DataContext!);
            SaveFileDialog dialog = new();
            dialog.Title = "��ѡ��Ҫ�����λ��";
            dialog.Filters = new() { new() { Name = "ģ���ļ�", Extensions = new() { "jar" } } };
            dialog.InitialFileName = mod.CurrentFileInfo.FileName;

            if (!string.IsNullOrEmpty(mod.ChineseName) && Regex.IsMatch(mod.ChineseName, @"[\u4e00-\u9fa5]")) {
                dialog.InitialFileName = $"[{mod.ChineseName.Split("(").FirstOrDefault().Trim()}] {mod.CurrentFileInfo.FileName}";
            }
            var res = await dialog.ShowAsync(MainWindow.win);

            if (!string.IsNullOrEmpty(res)) {
                TasksTooklit.CreateModDownloadTask(mod, (sender as Control)!, res);
            }
        }

        private void CancelButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {        
            SelectGameCoreDialog.Hide();
        }
    }
}
