using Microsoft.Extensions.DependencyInjection;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.EntityFrameworkCore.Clubs;
using Snooker.Interclub.EntityFrameworkCore.Teams;
using Snooker.Interclub.Teams;
using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Snooker.Interclub.EntityFrameworkCore;

[DependsOn(
    typeof(InterclubDomainModule),
    typeof(PlatformEntityFrameworkCoreModule))]
public class InterclubEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InterclubDbContext>(options =>
        {
            options.AddDefaultRepositories();
            options.AddRepository<Club, EfCoreClubRepository>();
            options.AddRepository<Team, EfCoreTeamRepository>();
        });
    }
}