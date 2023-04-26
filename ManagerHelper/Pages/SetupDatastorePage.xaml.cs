using ManagerHelper.ViewModels;

namespace ManagerHelper.Pages;

public partial class SetupDatastorePage : ContentPage
{
    public SetupDatastorePage(ISetupDatastoreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}