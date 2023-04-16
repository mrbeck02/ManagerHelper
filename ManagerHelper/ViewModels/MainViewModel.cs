using ManagerHelper.ViewModels.Support;

namespace ManagerHelper.ViewModels
{
    public class MainViewModel : PropertyChangedNotifier, IDisposable, IMainViewModel
    {
        //private IStatisticsCsvImporter _statisticsCsvImporter;
        //private IDbContextFactory<DataContext> _contextFactory;
        //private IStatisticsCsvReader _reader;

        #region Properties

        private string _csvPath = @"C:\Temp";

        public string CsvPath
        {
            get => _csvPath;
            set
            {
                if (string.CompareOrdinal(_csvPath, value) == 0)
                    return;

                _csvPath = value;
                OnPropertyChanged(nameof(CsvPath));
            }
        }

        //private ObservableCollection<ComboBoxItemViewModel<Developer>> _developerOptions;
        //private ComboBoxItemViewModel<Developer> _selectedDeveloperOption;

        //public ObservableCollection<ComboBoxItemViewModel<Developer>> DeveloperOptions
        //{
        //    get => _developerOptions;
        //    private set
        //    {
        //        _developerOptions = value;
        //        OnPropertyChanged(nameof(DeveloperOptions));
        //    }
        //}

        //public ComboBoxItemViewModel<Developer> SelectedDeveloperOption
        //{
        //    get => _selectedDeveloperOption;
        //    set
        //    {
        //        var origValue = _selectedDeveloperOption;

        //        var montageSelected = false;

        //        if (_developerOptions.Count > 0)
        //        {
        //            montageSelected = _developerOptions.Any(o => o.IsSelected);
        //        }

        //        // if we're setting the developer option to the same developer, skip it.
        //        if (value != null && _selectedDeveloperOption != null && value.Value == _selectedDeveloperOption.Value && montageSelected)
        //            return;


        //        if (value == null)
        //        {
        //            OnPropertyChanged(nameof(SelectedDeveloperOption));
        //            return;
        //        }

        //        foreach (var item in _developerOptions)
        //        {
        //            item.IsSelected = false;
        //        }

        //        _selectedDeveloperOption = value;
        //        _selectedDeveloperOption.IsSelected = true;

        //        OnPropertyChanged(nameof(SelectedDeveloperOption));
        //    }
        //}

        #endregion

        #region Commands
        // MAUI Commands: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/commanding?view=net-maui-7.0

        //public RelayCommand _importCsvCommand;


        //public RelayCommand ImportCsvCommand => _importCsvCommand ??= new RelayCommand(importCsvCommand);

        //private void importCsvCommand(object obj)
        //{
        //    try
        //    {
        //        var entries = _reader.ReadStatistics(CsvPath);

        //        if (entries.Count == 0) { 
        //            MessageBox.Show($"No entries found in file {CsvPath}", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //            return;
        //        }

        //        _statisticsCsvImporter.ImportData(entries, SelectedDeveloperOption.Value, new UnitOfWork(_contextFactory.CreateDbContext()));
        //        MessageBox.Show($"{entries.Count} entries imported.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        #endregion

        //public MainViewModel(IDbContextFactory<DataContext> contextFactory,
        //    IStatisticsCsvReader reader,
        //    IStatisticsCsvImporter statisticsCsvImporter)
        //{
        //    _statisticsCsvImporter = statisticsCsvImporter;
        //    _reader = reader;
        //    _contextFactory = contextFactory;

        //    initializeViewModel();
        //}

        //private void initializeViewModel()
        //{
        //    _developerOptions = createDeveloperOptions();

        //    if (_developerOptions.Count > 0)
        //    {
        //        _selectedDeveloperOption = _developerOptions[0];
        //        _selectedDeveloperOption.IsSelected = true;
        //    }
        //}

        //protected ObservableCollection<ComboBoxItemViewModel<Developer>> createDeveloperOptions()
        //{
        //    var unitOfWork = new UnitOfWork(_contextFactory.CreateDbContext());
        //    var allOptions = new List<ComboBoxItemViewModel<Developer>>();

        //    var developers = unitOfWork.DeveloperRepository.Get();
        //    allOptions.AddRange(developers.Select(m => new ComboBoxItemViewModel<Developer>(m, m.GetFullName())));
        //    allOptions.Sort();

        //    return new ObservableCollection<ComboBoxItemViewModel<Developer>>(
        //        allOptions.ToList());
        //}

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
