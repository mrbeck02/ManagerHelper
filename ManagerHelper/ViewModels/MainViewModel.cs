using ManagerHelper.CsvImporter;
using ManagerHelper.DAL;
using ManagerHelper.Data;
using ManagerHelper.Data.Entities;
using ManagerHelper.ViewModels.Support;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace ManagerHelper.ViewModels
{
    public class MainViewModel : PropertyChangedNotifier, IDisposable, IMainViewModel
    {
        private IStatisticsCsvImporter _statisticsCsvImporter;
        private IDbContextFactory<DataContext> _contextFactory;
        private IStatisticsCsvReader _reader;
        private IAlertService _alertService;

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
                refreshCanExecute(ImportCsvCommand);
            }
        }

        private List<Developer> _developerOptions;

        public List<Developer> DeveloperOptions
        {
            get => _developerOptions;
            private set
            {
                _developerOptions = value;
                OnPropertyChanged(nameof(DeveloperOptions));
            }
        }

        public Developer SelectedDeveloperOption { get; set; }

        #endregion

        #region Commands
        // MAUI Commands: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/commanding?view=net-maui-7.0

        public ICommand ImportCsvCommand { get; set; }

        #endregion

        public MainViewModel(IDbContextFactory<DataContext> contextFactory,
            IStatisticsCsvReader reader,
            IStatisticsCsvImporter statisticsCsvImporter,
            IAlertService alertService)
        {
            _statisticsCsvImporter = statisticsCsvImporter;
            _reader = reader;
            _contextFactory = contextFactory;
            _alertService = alertService;

            initializeViewModel();
        }

        private void initializeViewModel()
        {
            _developerOptions = createDeveloperOptions();

            if (_developerOptions.Count > 0)
            {
                SelectedDeveloperOption = _developerOptions[0];
            }

            createImportCsvCommand();
        }

        private void createImportCsvCommand()
        {
            ImportCsvCommand = new Command(
                execute: async () =>
                {
                    try
                    {
                        var entries = _reader.ReadStatistics(CsvPath);

                        if (entries.Count == 0)
                        {
                            _alertService.ShowAlert("Error", $"No entries found in file {CsvPath}");
                            return;
                        }

                        _statisticsCsvImporter.ImportData(entries, SelectedDeveloperOption, new UnitOfWork(_contextFactory.CreateDbContext()));
                        _alertService.ShowAlert("Success", $"{entries.Count} entries imported.");
                    }
                    catch (Exception ex)
                    {
                        _alertService.ShowAlert("Error", ex.Message);
                    }
                    refreshCanExecute(ImportCsvCommand);
                },
                canExecute: () =>
                {
                    return File.Exists(_csvPath);
                });

        }

        protected List<Developer> createDeveloperOptions()
        {
            var unitOfWork = new UnitOfWork(_contextFactory.CreateDbContext());

            var developers = unitOfWork.DeveloperRepository.Get().ToList();
            developers.Sort((a, b) => a.LastName.CompareTo(b.LastName));

            return developers;
        }

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
