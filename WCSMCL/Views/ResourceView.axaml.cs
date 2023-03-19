using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using MinecraftLaunch.Modules.Models.Download;
using MinecraftLaunch.Modules.Toolkits;
using System.Diagnostics;
using System.Linq;
using WCSMCL.Modules.Controls;
using WCSMCL.Modules.Models;
using WCSMCL.Modules.Toolkits;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class ResourceView : Page
    {
        public ResourceViewModel ViewModel { get; set; } = new();
        public ResourceView()
        {
            InitializeComponent();
            DataContext= ViewModel;
        }

        private void EnableButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Trace.WriteLine("[��Ϣ] ����Դ��������");
            var packViewdata = (sender as Avalonia.Controls.Button)!.DataContext as ResourcePackViewData<ResourcePack>;
            var res = JsonToolkit.GetEnableIndependencyCoreData(App.Data.FooterPath, PropertyView.GameCore.ToNatsurainkoGameCore());
            bool isolate = App.Data.Isolate;
            if (res != null && res.IsEnableIndependencyCore) {           
                isolate = res.Isolate;
            }

            ResourcePackToolkit toolkit = new(PropertyView.GameCore, false, isolate, App.Data.FooterPath);
            ViewModel.DisbaledResourcePacks.Remove(packViewdata);
            ViewModel.EnabledResourcePacks.Add(packViewdata);
            toolkit.EnabledResourcePacks(ViewModel.EnabledResourcePacks.Select(x => x.Data));
        }

        private void DisbaledButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Trace.WriteLine("[��Ϣ] ����Դ���ѽ���");
            var packViewdata = (sender as Avalonia.Controls.Button)!.DataContext as ResourcePackViewData<ResourcePack>;
            var res = JsonToolkit.GetEnableIndependencyCoreData(App.Data.FooterPath, PropertyView.GameCore.ToNatsurainkoGameCore());
            bool isolate = App.Data.Isolate;
            if (res != null && res.IsEnableIndependencyCore)
            {
                isolate = res.Isolate;
            }

            ResourcePackToolkit toolkit = new(PropertyView.GameCore, false, isolate, App.Data.FooterPath);
            ViewModel.EnabledResourcePacks.Remove(packViewdata);
            ViewModel.DisbaledResourcePacks.Add(packViewdata);
            toolkit.EnabledResourcePacks(ViewModel.EnabledResourcePacks.Select(x => x.Data));
        }

        private void MoveUpButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Trace.WriteLine("[��Ϣ] ����Դ�����ȶ����ϵ�");
            var packViewdata = (sender as Avalonia.Controls.Button)!.DataContext as ResourcePackViewData<ResourcePack>;
            var res = JsonToolkit.GetEnableIndependencyCoreData(App.Data.FooterPath, PropertyView.GameCore.ToNatsurainkoGameCore());
            bool isolate = App.Data.Isolate;
            if (res != null && res.IsEnableIndependencyCore) {            
                isolate = res.Isolate;
            }

            ResourcePackToolkit toolkit = new(PropertyView.GameCore, false, isolate, App.Data.FooterPath);
            int index = ViewModel.EnabledResourcePacks.IndexOf(packViewdata);
            if (index != 0)
            {
                ViewModel.EnabledResourcePacks.Remove(packViewdata);
                ViewModel.EnabledResourcePacks.Insert(index - 1, packViewdata);
            }
            toolkit.EnabledResourcePacks(ViewModel.EnabledResourcePacks.Select(x => x.Data));
        }

        private void MoveDownButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Trace.WriteLine("[��Ϣ] ����Դ�����ȶ����µ�");
            var packViewdata = (sender as Avalonia.Controls.Button)!.DataContext as ResourcePackViewData<ResourcePack>;
            var res = JsonToolkit.GetEnableIndependencyCoreData(App.Data.FooterPath, PropertyView.GameCore.ToNatsurainkoGameCore());
            bool isolate = App.Data.Isolate;
            if (res != null && res.IsEnableIndependencyCore)
            {
                isolate = res.Isolate;
            }

            ResourcePackToolkit toolkit = new(PropertyView.GameCore, false, isolate, App.Data.FooterPath);
            int index = ViewModel.EnabledResourcePacks.IndexOf(packViewdata);
            if (index != ViewModel.EnabledResourcePacks.Count - 1)
            {
                ViewModel.EnabledResourcePacks.Remove(packViewdata);
                ViewModel.EnabledResourcePacks.Insert(index + 1, packViewdata);
            }
            toolkit.EnabledResourcePacks(ViewModel.EnabledResourcePacks.Select(x => x.Data));
        }

        public override void OnNavigatedTo()
        {
            ViewModel.LoadAllPacksAction();
        }
    }
}
