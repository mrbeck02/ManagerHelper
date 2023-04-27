using ManagerHelper.ViewModels;

namespace ManagerHelper.Pages;

public partial class SetupJiraPage : ContentPage
{
    public SetupJiraPage(ISetupJiraViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}