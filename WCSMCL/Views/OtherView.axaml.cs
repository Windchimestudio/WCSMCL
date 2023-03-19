using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WCSMCL.Modules.Controls;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class OtherView : Page
    {
        public OtherView()
        {
            InitializeComponent(true);
            DataContext = new OtherViewModel();
        }
    }
}
