using Microsoft.EntityFrameworkCore;

namespace ManagerHelper.Data
{
    internal class SqliteDataContextFactory : ISqliteDataContextFactory<DataContext>
    {
        public string DbPath { get; set; }

        public DataContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite(getConnectionString());
            return new DataContext(optionsBuilder.Options);
        }

        private string getConnectionString()
        {
            if (!File.Exists(DbPath))
                throw new FileNotFoundException($"Database not detected at the given location: {DbPath}");

            return $"Data Source={DbPath}";
        }
    }
}
