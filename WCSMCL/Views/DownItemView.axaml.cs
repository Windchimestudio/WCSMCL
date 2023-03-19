using Natsurainko.Toolkits.Network.Model;
using System.Collections.Generic;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Enum;
using WCSMCL.Modules.Interface;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public delegate void AfterDo();
    public partial class DownItemView : Page, ITask
    {

        public DownItemView() => InitializeComponent();

        public DownItemView(string Id,DownType Type)
        {
            InitializeComponent(Id, Type);
        }

        public DownItemView(HttpDownloadRequest http)
        {
            InitializeComponent(http);
        }

        public DownItemView(HttpDownloadRequest http, string TaskName)
        {
            InitializeComponent(http, TaskName);
        }

        public DownItemView(HttpDownloadRequest http, string TaskName, AfterDo AfterDo)
        {
            InitializeComponent(http, TaskName, AfterDo);
        }

        public DownItemView(List<ModLoaderInformationViewData> Id)
        {
            InitializeComponent(Id);
        }

        public DownItemView(string Id)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id);
        }
      
        public DownItemView(ModLoaderInformationViewData Id)
        {
            InitializeComponent(Id);
        }

        private void InitializeComponent(string Id, DownType Type)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id, Type);
        }

        private void InitializeComponent(List<ModLoaderInformationViewData> Id)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id);
        }

        private void InitializeComponent(HttpDownloadRequest Id)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id);
        }

        private void InitializeComponent(HttpDownloadRequest Id, string TaskName)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id, TaskName);
        }

        private void InitializeComponent(HttpDownloadRequest Id, string TaskName, AfterDo AfterDo)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id, TaskName, AfterDo);
        }

        private void InitializeComponent(ModLoaderInformationViewData Id)
        {
            InitializeComponent(true);
            DataContext = new DownItemViewModel(Id);
        }

        public void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TaskView.Remove(this);
            MainView.ViewModel.AllTaskCount--;
        }
    }
}
