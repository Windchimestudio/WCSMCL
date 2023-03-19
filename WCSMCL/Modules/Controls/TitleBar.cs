using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;
using WCSMCL.Modules.Const;
using Button = Avalonia.Controls.Button;

namespace WCSMCL.Modules.Controls
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
    public partial class TitleBar : TemplatedControl
    {
        public static readonly StyledProperty<ICommand> CloseButtonCommandProperty =
            AvaloniaProperty.Register<TitleBar, ICommand>(nameof(CloseButtonCommand));

        public static readonly StyledProperty<ICommand> MaxButtonCommandProperty =
            AvaloniaProperty.Register<TitleBar, ICommand>(nameof(MaxButtonCommand));

        public static readonly StyledProperty<ICommand> MiniButtonCommandProperty =
            AvaloniaProperty.Register<TitleBar, ICommand>(nameof(MiniButtonCommand));

        public static readonly StyledProperty<object> ContentProperty =
            ContentControl.ContentProperty.AddOwner<TitleBar>();        
    }

    partial class TitleBar
    {
        public ICommand CloseButtonCommand
        {
            get => GetValue(CloseButtonCommandProperty);
            set => SetValue(CloseButtonCommandProperty, value);
        }

        public ICommand MaxButtonCommand
        {
            get => GetValue(MaxButtonCommandProperty);
            set => SetValue(MaxButtonCommandProperty, value);
        }

        public ICommand MiniButtonCommand
        {
            get => GetValue(MiniButtonCommandProperty);
            set => SetValue(MiniButtonCommandProperty, value);
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// 活动窗口
        /// </summary>
        protected Window? HostWindow { get; private set; }
    }

    partial class TitleBar
    {
        public virtual void Attach(Window hostWindow)
        {
            if (_disposables == null)
            {
                HostWindow = hostWindow;

                _disposables = new CompositeDisposable
                {
                    HostWindow.GetObservable(Window.WindowStateProperty)
                    .Subscribe(x =>
                    {
                        PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                        PseudoClasses.Set(":normal", x == WindowState.Normal);
                        PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                        PseudoClasses.Set(":fullscreen", x == WindowState.Maximized);
                    })
                };
            }
        }

        public virtual void Detach()
        {
            if (_disposables != null)
            {
                _disposables.Dispose();
                _disposables = null;

                HostWindow = null;
            }
        }

        public virtual void OnClose()
        {
            HostWindow?.Close();           
        }

        public virtual void OnRestore()
        {
            if (HostWindow != null)
            {
                HostWindow.WindowState = HostWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }

            if (HostWindow.WindowState is WindowState.Maximized)
                _RestoreButtonPath.Data = StreamGeometry.Parse("M2048 410h-410v-410h-1638v1638h410v410h1638v-1638zM1434 1434h-1229v-1229h1229v1229zM1843 1843h-1229v-205h1024v-1024h205v1229z");
            else _RestoreButtonPath.Data = StreamGeometry.Parse("M2048 2048v-2048h-2048v2048h2048zM1843 1843h-1638v-1638h1638v1638z");
        }

        public virtual void OnMinimize()
        {
            if (HostWindow != null) {            
                HostWindow.WindowState = WindowState.Minimized;
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            BugHelp = e.NameScope.Get<MenuFlyoutItem>("BugHelp");
            GitHelp = e.NameScope.Get<MenuFlyoutItem>("GitHelp");
            helpbutton = e.NameScope.Get<Button>("Help");
            closebutton = e.NameScope.Get<Button>("Close");
            maxbutton = e.NameScope.Get<Button>("Max");
            minibutton = e.NameScope.Get<Button>("Mini");
            if (InfoConst.IsWindows || InfoConst.IsLinux)
                _RestoreButtonPath = e.NameScope.Get<Path>("RestoreButtonPath");
            closebutton.Click += Closebutton_Click;
            minibutton.Click += Minibutton_Click;
            maxbutton.Click += Maxbutton_Click;            
            HostWindow.PropertyChanged += HostWindow_PropertyChanged;
            BugHelp.Click += BugHelp_Click;
            GitHelp.Click += GitHelp_Click;
            //closebutton.PointerPressed += (_, _) => OnClose();
            //minibutton.PointerPressed += (_, _) => OnMinimize();
            //maxbutton.PointerPressed += (_, _) => OnRestore();
        }

        private async void GitHelp_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Run(() => {
                using var res = Process.Start(new ProcessStartInfo("https://github.com/Blessing-Studio")
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            });
        }

        private async void BugHelp_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Run(() => {
                using var res = Process.Start(new ProcessStartInfo("https://github.com/Blessing-Studio/WCSMCL/issues")
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            });
        }

        private void HostWindow_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name is "WindowState")
            {
                if (((WindowState)e.NewValue) is WindowState.Maximized)
                    _RestoreButtonPath.Data = StreamGeometry.Parse("M2048 410h-410v-410h-1638v1638h410v410h1638v-1638zM1434 1434h-1229v-1229h1229v1229zM1843 1843h-1229v-205h1024v-1024h205v1229z");
                else _RestoreButtonPath.Data = StreamGeometry.Parse("M2048 2048v-2048h-2048v2048h2048zM1843 1843h-1638v-1638h1638v1638z");
            }
        }

        private void Maxbutton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MaxButtonCommand.Execute(null);
        }

        private void Minibutton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MiniButtonCommand.Execute(null);
        }

        private void Closebutton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            CloseButtonCommand.Execute(null);
        }
    }

    partial class TitleBar
    {
        private MenuFlyoutItem BugHelp;
        private MenuFlyoutItem GitHelp;
        private Button closebutton;
        private Button helpbutton;
        private Button maxbutton;
        private Button minibutton;
        private Path _RestoreButtonPath;
        private CompositeDisposable? _disposables;
    }
}
