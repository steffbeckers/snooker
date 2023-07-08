using Snooker.Interclub;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Snooker.Clubs;

public class ClubManager : DomainService
{
    private readonly IClubRepository _clubRepository;

    public ClubManager(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<Club> CreateAsync(Guid id, string name)
    {
        if (await _clubRepository.AnyAsync(x => x.Name == name))
        {
            throw new BusinessException(InterclubErrorCodes.Clubs.AlreadyExists);
        }

        return new Club(id, name);
    }
}