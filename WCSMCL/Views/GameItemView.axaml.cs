using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using WCSMCL.Modules.Base;
using WCSMCL.Modules.Controls;
using WCSMCL.ViewModels;

namespace WCSMCL.Views
{
    public partial class GameItemView : Page
    {
        public GameItemView(GameItemViewModel model) => InitializeComponent(model);
        public GameItemView() => InitializeComponent();

        private void InitializeComponent(GameItemViewModel model)
        {
            InitializeComponent(true);
            this.DataContext = model;
            PointerEnter += model.MoveInAction;
            PointerLeave += model.MoveOutAction;
        }
    }
}
