using Microsoft.EntityFrameworkCore;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Players;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using Snooker.Platform.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Snooker.Interclub.EntityFrameworkCore;

[ConnectionStringName(InterclubDbProperties.ConnectionStringName)]
public interface IInterclubDbContext : IEfCoreDbContext, IPlatformDbContext
{
    public DbSet<Club> Clubs { get; }

    public DbSet<Division> Divisions { get; }

    public DbSet<Frame> Frames { get; }

    public DbSet<Match> Matches { get; }

    public DbSet<MatchTeamPlayer> MatchTeamPlayers { get; }

    public DbSet<Player> Players { get; }

    public DbSet<Season> Seasons { get; }

    public DbSet<TeamPlayer> TeamPlayers { get; }

    public DbSet<Team> Teams { get; }
}