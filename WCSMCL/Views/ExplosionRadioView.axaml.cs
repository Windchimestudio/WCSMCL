using Avalonia.Controls;
using WCSMCL.Modules.Controls;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class ExplosionRadioView : Page
    {
        public ExplosionRadioView() {
       
            InitializeComponent();
            DataContext = new ExplosionRadioViewModel();
        }
    }
}
