using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Snooker.Interclub.EntityFrameworkCore;

[DependsOn(
    typeof(InterclubTestBaseModule),
    typeof(InterclubEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqliteModule))]
public class InterclubEntityFrameworkCoreTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysDisableUnitOfWorkTransaction();

        SqliteConnection sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(abpDbContextConfigurationContext =>
            {
                abpDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
                abpDbContextConfigurationContext.DbContextOptions.EnableSensitiveDataLogging();
            });
        });
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        new InterclubDbContext(
            new DbContextOptionsBuilder<InterclubDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}