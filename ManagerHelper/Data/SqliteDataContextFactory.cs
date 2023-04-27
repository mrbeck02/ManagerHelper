using Microsoft.EntityFrameworkCore;

namespace ManagerHelper.Data
{
    internal class SqliteDataContextFactory : ISqliteDataContextFactory<DataContext>
    {
        public string DbPath { get; set; }

        /// <summary>
        /// Creates a dbcontxt for an SQLite database.  Be sure to set the DbPath to the location of the database.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown when db not found</exception>
        /// <returns></returns>
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
