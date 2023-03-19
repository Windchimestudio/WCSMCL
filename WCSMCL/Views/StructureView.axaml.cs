using Avalonia.Controls;
using WCSMCL.Modules.Controls;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class StructureView : Page
    {
        public StructureViewModel ViewModel { get; set; } = new();
        public StructureView()
        {
            InitializeComponent(true);
            DataContext = ViewModel;
        }
    }
}
