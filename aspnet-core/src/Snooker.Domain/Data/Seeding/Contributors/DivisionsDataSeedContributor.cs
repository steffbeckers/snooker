// TODO
//using System.Threading.Tasks;
//using Volo.Abp.Data;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.TenantManagement;

//namespace Snooker.Data.Seeding.Contributors;

//public class DivisionsDataSeedContributor :
//    IDataSeedContributor,
//    ITransientDependency
//{
//    private readonly ITenantRepository _tenantRepository;

//    public DivisionsDataSeedContributor(ITenantRepository tenantRepository)
//    {
//        _tenantRepository = tenantRepository;
//    }

//    public async Task SeedAsync(DataSeedContext context)
//    {
//        if (!context.TenantId.HasValue)
//        {
//            return;
//        }

//        Tenant tenant = await _tenantRepository.GetAsync(context.TenantId.Value);

//        if (tenant.Name == "Limburg")
//        {
//        }
//    }
//}