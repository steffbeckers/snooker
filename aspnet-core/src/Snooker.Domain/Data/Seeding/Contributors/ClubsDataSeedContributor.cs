using Newtonsoft.Json;
using Snooker.Addresses;
using Snooker.Clubs;
using Snooker.Players;
using Snooker.TeamPlayers;
using Snooker.Teams;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.TenantManagement;

namespace Snooker.Data.Seeding.Contributors;

public class ClubsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ClubManager _clubManager;
    private readonly IClubRepository _clubRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ITeamRepository _teamRepository;
    private readonly ITenantRepository _tenantRepository;

    public ClubsDataSeedContributor(
        ClubManager clubManager,
        IClubRepository clubRepository,
        IGuidGenerator guidGenerator,
        ITeamRepository teamRepository,
        ITenantRepository tenantRepository)
    {
        _clubManager = clubManager;
        _clubRepository = clubRepository;
        _guidGenerator = guidGenerator;
        _teamRepository = teamRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (!context.TenantId.HasValue)
        {
            return;
        }

        Tenant tenant = await _tenantRepository.GetAsync(context.TenantId.Value);

        if (tenant.Name == "Limburg")
        {
            string limburgClubsJson = await File.ReadAllTextAsync("Data/Seeding/Clubs/snookerlimburg.be.json");
            List<WebScraper.Clubs.Club> limburgClubs = JsonConvert.DeserializeObject<List<WebScraper.Clubs.Club>>(limburgClubsJson);

            foreach (WebScraper.Clubs.Club limburgClub in limburgClubs)
            {
                Club? club = await _clubRepository.FindAsync(x => x.Name == limburgClub.Name);

                if (club == null)
                {
                    club = await _clubManager.CreateAsync(
                        id: _guidGenerator.Create(),
                        name: limburgClub.Name);
                    club.Email = limburgClub.Email;
                    club.PhoneNumber = limburgClub.PhoneNumber;
                    club.Website = limburgClub.Website;
                    club.Address = new Address()
                    {
                        Street = limburgClub.Address?.Street,
                        Number = limburgClub.Address?.Number,
                        PostalCode = limburgClub.Address?.PostalCode,
                        City = limburgClub.Address?.City
                    };

                    foreach (WebScraper.Teams.Team limburgTeam in limburgClub.Teams)
                    {
                        Team team = new Team(_guidGenerator.Create(), limburgTeam.Name)
                        {
                            ClubId = club.Id
                        };

                        foreach (WebScraper.Players.Player limburgPlayer in limburgTeam.Players)
                        {
                            Player? player = club.Players.FirstOrDefault(x =>
                                x.FirstName == limburgPlayer.FirstName &&
                                x.LastName == limburgPlayer.LastName &&
                                x.DateOfBirth == limburgPlayer.DateOfBirth);

                            if (player == null)
                            {
                                player = new Player(
                                    id: _guidGenerator.Create(),
                                    firstName: limburgPlayer.FirstName,
                                    lastName: limburgPlayer.LastName)
                                {
                                    ClubId = club.Id,
                                    Class = limburgPlayer.Class,
                                    DateOfBirth = limburgPlayer.DateOfBirth
                                };

                                club.Players.Add(player);
                            }

                            TeamPlayer teamPlayer = new TeamPlayer(
                                id: _guidGenerator.Create(),
                                team.Id,
                                player.Id);

                            team.Players.Add(teamPlayer);
                        }

                        club.Teams.Add(team);
                    }

                    await _clubRepository.InsertAsync(club);
                }
            }
        }
    }
}