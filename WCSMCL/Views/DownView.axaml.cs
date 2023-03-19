using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using Natsurainko.Toolkits.Network.Model;
using PluginLoader;
using System;
using System.Threading.Tasks;
using WCSMCL.Modules.Const;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Toolkits;
using WCSMCL.PluginAPI;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class DownView : Page
    {
        public DownViewModel ViewModel { get; } = new DownViewModel();
        public DownView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            InitializeComponent(true);           
            if (!InfoConst.IsWindows) {
                JavaInstall.IsVisible = false;
            }

            DataContext = ViewModel;
            JavaInstallDialog.DataContext = ViewModel;
            JavaInstall.PointerPressed += JavaInstall_PointerPressed;
            DownloadDialog.DataContext = ViewModel;
            CustomDownload.PointerPressed += CustomDownload_PointerPressed;
        }
        private async void CustomDownload_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e) => await DownloadDialog.ShowAsync();
        private async void DownView_Initialized(object? sender, System.EventArgs e)
        {
            await Task.Delay(500);
            JavaInstallDialog.IsVisible = true;
        }

        private async void JavaInstall_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e) {
            if (InfoConst.IsWindows) {
                await JavaInstallDialog.ShowAsync();
            }
        }

        private void JavaCancelButton_Click(object sender, RoutedEventArgs e)
        {
            JavaInstallDialog.Hide();
            MainWindow.ShowInfoBarAsync(LanguageToolkit.GetText("Info"), LanguageToolkit.GetText("CancelInstallJavaRunTime"), severity: InfoBarSeverity.Informational);
        }

        private void JavaConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            JavaInstallDialog.Hide();
            TasksTooklit.CreateJavaInstallTask(ViewModel.CurrentUrl.Value.Value, sender as Control);
        }
        private void CustomDownloadCancelButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadDialog.Hide();
            MainWindow.ShowInfoBarAsync($"信息：", "已取消下载", severity: InfoBarSeverity.Informational);
        }

        private void CustomDownloadConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.DownloadUrl != "下载链接" && ViewModel.FileName != "保存的文件名") {
                DownloadDialog.Hide();
                HttpDownloadRequest httpDownloadRequest = new();
                httpDownloadRequest.Directory = new System.IO.DirectoryInfo(Environment.CurrentDirectory);
                httpDownloadRequest.Url = ViewModel.DownloadUrl;
                httpDownloadRequest.FileName = App.Data.CustomDownloadPath;
                var HttpDownloadEvent = new HttpDownloadEvent(httpDownloadRequest, "自定义下载");
                Event.CallEvent(HttpDownloadEvent);
            }
            MainWindow.ShowInfoBarAsync($"信息：", "下载链接和文件名不能为空", severity: InfoBarSeverity.Informational);
        }
    }
}
