using ManagerHelper.ViewModels;

namespace ManagerHelper;

public partial class MainPage : ContentPage
{
	public MainPage(IMainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}

