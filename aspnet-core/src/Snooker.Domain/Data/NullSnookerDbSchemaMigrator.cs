using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Snooker.Data
{
    // This is used if database provider doesn't define ISnookerDbSchemaMigrator implementation.
    public class NullSnookerDbSchemaMigrator : ISnookerDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}