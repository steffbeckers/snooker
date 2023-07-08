using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Snooker.Data;

public class NullSnookerDbSchemaMigrator : ISnookerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}