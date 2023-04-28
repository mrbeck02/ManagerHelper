using ManagerHelper.ViewModels;

namespace ManagerHelper.Pages;

public partial class TeamVelocityPage : ContentPage
{
	public TeamVelocityPage(ITeamVelocityViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
