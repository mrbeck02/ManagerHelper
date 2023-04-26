using ManagerHelper.ViewModels;

namespace ManagerHelper.Pages;

public partial class ImportFromCsvPage : ContentPage
{
	public ImportFromCsvPage(IImportFromCsvViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}