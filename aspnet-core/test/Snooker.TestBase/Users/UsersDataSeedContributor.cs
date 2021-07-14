using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Snooker.Players
{
    public class UsersDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityUserRepository _identityUserRepository;

        public UsersDataSeedContributor(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            IdentityUser player = new IdentityUser(
                Guid.Parse("4a05a121-7e89-4998-bb46-9d88cc49973f"),
                "player@snooker.com",
                "player@snooker.com");

            await _identityUserRepository.InsertAsync(player);
        }
    }
}