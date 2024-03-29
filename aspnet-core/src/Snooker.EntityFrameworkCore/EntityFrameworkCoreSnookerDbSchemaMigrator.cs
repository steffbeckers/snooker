using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snooker.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Snooker.EntityFrameworkCore;

public class EntityFrameworkCoreSnookerDbSchemaMigrator : ISnookerDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSnookerDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        await _serviceProvider
            .GetRequiredService<SnookerDbContext>()
            .Database
            .MigrateAsync();
    }
}