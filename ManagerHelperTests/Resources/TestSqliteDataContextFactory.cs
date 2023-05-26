using ManagerHelper.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ManagerHelperTests.Resources
{
    internal class TestSqliteDataContextFactory : ISqliteDataContextFactory<DataContext>, IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<DataContext> _contextOptions;

        public string DbPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TestSqliteDataContextFactory()
        { 
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            using var context = new DataContext(_contextOptions);
            context.Database.EnsureCreated();
        }

        public DataContext CreateDbContext() => new DataContext(_contextOptions);

        public void Dispose() => _connection.Dispose();
    }
}
