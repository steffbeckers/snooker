using System.Threading.Tasks;

namespace Snooker.Data
{
    public interface ISnookerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
