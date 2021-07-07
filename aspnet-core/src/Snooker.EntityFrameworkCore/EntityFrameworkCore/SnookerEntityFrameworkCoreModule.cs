using Microsoft.Extensions.DependencyInjection;
using Snooker.Clubs;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Snooker.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(SnookerDomainModule))]
    public class SnookerEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SnookerDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                //options.AddDefaultRepositories(includeAllEntities: true);
                options.AddDefaultRepositories();

                options.AddRepository<Club, EfCoreClubRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also SnookerMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SnookerEfCoreEntityExtensionMappings.Configure();
        }
    }
}