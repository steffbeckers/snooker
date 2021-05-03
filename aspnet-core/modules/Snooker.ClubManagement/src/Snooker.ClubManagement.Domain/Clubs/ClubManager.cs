using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Snooker.ClubManagement.Clubs
{
    public class ClubManager : DomainService
    {
        private readonly IClubRepository _clubRepository;

        public ClubManager(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task<Club> CreateAsync([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Club existingClub = await _clubRepository.FindByNameAsync(name);
            if (existingClub != null)
            {
                throw new ClubAlreadyExistsException(name);
            }

            return new Club(
                GuidGenerator.Create(),
                name
            );
        }
    }
}
