using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ManagerHelper.Data
{
    /// <summary>
    /// Design time DataContext factory used when creating migrations
    /// Credit: https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    /// </summary>
    //internal class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<DataContext>
    //{
    //    public DataContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilder.UseSqlite("Data Source=c:\\Temp\\mydb.db");
    //        return new DataContext(optionsBuilder.Options);
    //    }
    //}
}
