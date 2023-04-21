using ManagerHelper.ViewModels;

namespace ManagerHelper;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(IMainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}

