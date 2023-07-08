using Microsoft.Extensions.DependencyInjection;
using Snooker.Clubs;
using Snooker.EntityFrameworkCore.Clubs;
using Snooker.EntityFrameworkCore.Teams;
using Snooker.Interclub.EntityFrameworkCore;
using Snooker.Platform.EntityFrameworkCore;
using Snooker.Teams;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Snooker.EntityFrameworkCore;

[DependsOn(
    typeof(InterclubEntityFrameworkCoreModule),
    typeof(PlatformEntityFrameworkCoreModule),
    typeof(SnookerDomainModule))]
public class SnookerEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // TODO: Move to Interclub module
        context.Services.AddAbpDbContext<SnookerDbContext>(options =>
        {
            options.AddDefaultRepositories();
            options.AddRepository<Club, EfCoreClubRepository>();
            options.AddRepository<Team, EfCoreTeamRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });
    }

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SnookerEfCoreEntityExtensionMappings.Configure();
    }
}