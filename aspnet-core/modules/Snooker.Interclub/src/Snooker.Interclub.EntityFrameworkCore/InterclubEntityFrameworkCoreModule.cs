using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.EntityFrameworkCore.Clubs;
using Snooker.Interclub.EntityFrameworkCore.Divisions;
using Snooker.Interclub.EntityFrameworkCore.Frames;
using Snooker.Interclub.EntityFrameworkCore.Matches;
using Snooker.Interclub.EntityFrameworkCore.Players;
using Snooker.Interclub.EntityFrameworkCore.Seasons;
using Snooker.Interclub.EntityFrameworkCore.Teams;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Players;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
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
            options.AddRepository<Division, EfCoreDivisionRepository>();
            options.AddRepository<Frame, EfCoreFrameRepository>();
            options.AddRepository<Match, EfCoreMatchRepository>();
            options.AddRepository<Player, EfCorePlayerRepository>();
            options.AddRepository<Season, EfCoreSeasonRepository>();
            options.AddRepository<Team, EfCoreTeamRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.PreConfigure<InterclubDbContext>(preConfigureOptions =>
            {
                preConfigureOptions.DbContextOptions.UseLazyLoadingProxies();
            });
        });
    }
}