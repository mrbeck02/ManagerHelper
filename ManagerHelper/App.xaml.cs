using System.Diagnostics;

namespace ManagerHelper;

public partial class App : Application
{
	public App()
	{
        AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
		InitializeComponent();

		MainPage = new AppShell();
	}

    private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        // This is global uncaught exception logging
        Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Exception}");
    }
}