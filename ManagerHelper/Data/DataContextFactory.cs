using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ManagerHelper.Data
{
    /// <summary>
    /// Design time DataContext factory used when creating migrations
    /// Credit: https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    /// </summary>
    internal class DataContextFactory : IDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite("Data Source=c:\\Temp\\mydb.db");
            return new DataContext(optionsBuilder.Options);
        }
    }
}
