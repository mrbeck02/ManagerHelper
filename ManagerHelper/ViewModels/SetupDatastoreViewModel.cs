using ManagerHelper.Resources;
using ManagerHelper.ViewModels.Support;
using System.Text;
using System.Windows.Input;

namespace ManagerHelper.ViewModels
{
    public class SetupDatastoreViewModel : PropertyChangedNotifier, ISetupDatastoreViewModel
    {
        private IAlertService _alertService;
        private string _dbPath = "";

        public string DbPath
        {
            get => _dbPath;
            set
            {
                if (string.CompareOrdinal(_dbPath, value) == 0)
                    return;

                _dbPath = value;
                Preferences.Default.Set(PreferenceKey.db_location.ToString(), value);
                //setupContextFactory();
                OnPropertyChanged(nameof(DbPath));
            }
        }

        public ICommand SelectDbCommand { get; set; }

        public SetupDatastoreViewModel(IAlertService alertService)
        {
            _alertService = alertService;
            initializeViewModel();
        }

        private void initializeViewModel()
        {
            createSelectDbCommand();
            setupDbPath();
        }

        private void setupDbPath()
        {
            _dbPath = Preferences.Default.Get(PreferenceKey.db_location.ToString(), "");
        }

        private void createSelectDbCommand()
        {
            SelectDbCommand = new Command(
                execute: async () =>
                {
                    PickOptions options = new()
                    {
                        PickerTitle = "Select SQLite File"
                    };

                    var result = await FilePicker.Default.PickAsync(options);

                    if (!File.Exists(result.FullPath))
                    {
                        _alertService.ShowAlert("Error", $"The given file does not exist. {result.FullPath}");                        
                        return;
                    }

                    if (!isValidSqliteFile(result.FullPath))
                    {
                        _alertService.ShowAlert("Error", $"The given file is not an SQLite db. {result.FullPath}");
                        return;
                    }

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        DbPath = result.FullPath;
                    });
                });
        }

        private bool isValidSqliteFile(string path)
        {
            byte[] bytes = new byte[17];
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Read(bytes, 0, 16);
            }
            string chkStr = ASCIIEncoding.ASCII.GetString(bytes);
            return chkStr.Contains("SQLite format");
        }


        //private void setupContextFactory()
        //{
        //    _contextFactory.DbPath = DbPath;
        //}
    }
}
