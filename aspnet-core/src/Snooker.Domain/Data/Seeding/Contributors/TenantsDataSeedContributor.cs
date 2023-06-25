using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TenantManagement;

namespace Snooker.Data.Seeding.Contributors;

public class TenantsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ITenantManager _tenantManager;
    private readonly ITenantRepository _tenantRepository;

    public TenantsDataSeedContributor(
        ITenantManager tenantManager,
        ITenantRepository tenantRepository)
    {
        _tenantManager = tenantManager;
        _tenantRepository = tenantRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        Tenant? tenantLimburg = await _tenantRepository.FindByNameAsync("Limburg");

        if (tenantLimburg == null)
        {
            tenantLimburg = await _tenantManager.CreateAsync("Limburg");

            await _tenantRepository.InsertAsync(tenantLimburg);
        }
    }
}