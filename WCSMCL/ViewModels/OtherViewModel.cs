using System.Threading.Tasks;
using WCSMCL.Modules.Base;
using GithubLib;
using FluentAvalonia.UI.Controls;
using static WCSMCL.MainWindow;
using PluginLoader;
using WCSMCL.PluginAPI;
using WCSMCL.Modules.Const;
using WCSMCL.Modules.Controls;

namespace WCSMCL.ViewModels
{
    public partial class OtherViewModel : ViewModelBase
    {
        public async Task Check()
        {
            if (!InfoConst.IsDevelopVersion)
            {
                Event.CallEvent(new UpdataCheckEvent());
                try
                {
                    await Task.Run(async () =>
                    {
                        IsCheckVersion = true;
                        await Task.Delay(2000);
                        IsCheckVersion = false;
                        string releaseUrl = GithubLib.GithubLib.GetRepoLatestReleaseUrl("Windchimestudio", "WCSMCL");
                        Release? release = GithubLib.GithubLib.GetRepoLatestRelease(releaseUrl);
                        if (release != null)
                        {
                            if (release.name != "")
                            {
                                ShowInfoBarAsync("自动更新", "发现新版本" + release.name + "  当前版本" + InfoConst.LauncherVersion + "  ", InfoBarSeverity.Informational);
                            }
                        }
                    });
                }
                catch { }
            }
            else
            {
                //ShowInfoBarAsync("提示", "当前版本为Dev版本 自动更新已关闭", InfoBarSeverity.Informational);
            }
        }

        public void NavigatedToStructureView()
        {
            Page.NavigatedToStructureView();
        }
    }

    partial class OtherViewModel
    {
        public string Version
        {
            get => _Version;
            set => RaiseAndSetIfChanged(ref _Version, value);
        }
        public string ButtonContent
        {
            get => _ButtonContent;
            set => RaiseAndSetIfChanged(ref _ButtonContent, value);
        }

        public bool IsCheckVersion
        {
            get => _IsCheckVersion;
            set => RaiseAndSetIfChanged(ref _IsCheckVersion, value);
        }

        public string CurrentVersion => $"当前版本号：{VersionPrefix}{Version}"; 
        public string VersionPrefix => $"Lsaac "; 
    }

    partial class OtherViewModel
    {
        public string _Version = InfoConst.LauncherVersion; 
        public string _ButtonContent = "检查更新";
        public bool _IsCheckVersion = false;
    }
}
