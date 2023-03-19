using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using SkiaSharp;
using System;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Toolkits;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class DownSettingView : Page
    {
        public DownSettingView()
        {
            InitializeComponent(true);
            DataContext = new DownSettingViewModel();
        }
        public async void OpenFolderDialog(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                OpenFolderDialog dialog;
                dialog = new OpenFolderDialog
                {
                    Title = "��ѡ������·��",
                    DefaultDirectory = Environment.CurrentDirectory
                };

                var result = await dialog.ShowAsync(MainWindow.win);

                if (result is not null)
                {
                    App.Data.CustomDownloadPath = result;
                    ((DownSettingViewModel)DataContext).DownloadPath = result;
                }
                JsonToolkit.JsonWrite();
            }
            catch (Exception ex)
            {
                MainWindow.ShowInfoBarAsync("����", $"���������벻���Ĵ���\n{ex}", InfoBarSeverity.Error);
            }
        }
    }
}


    //resm:WCSMCL.Resources.HarmonyOS#HarmonyOS Sans