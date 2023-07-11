using Microsoft.EntityFrameworkCore;
using Snooker.Interclub.Addresses;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Players;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Snooker.Interclub.EntityFrameworkCore;

public static class InterclubDbContextModelCreatingExtensions
{
    public static void ConfigureInterclub(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Club>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Clubs", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(ClubConsts.NameMaxLength);
            b.Property(x => x.Email).HasMaxLength(ClubConsts.EmailMaxLength);
            b.Property(x => x.PhoneNumber).HasMaxLength(ClubConsts.PhoneNumberMaxLength);
            b.Property(x => x.Website).HasMaxLength(ClubConsts.WebsiteMaxLength);
            b.OwnsOne(
                x => x.Address,
                b =>
                {
                    b.ToTable(InterclubDbProperties.DbTablePrefix + "ClubAddresses", InterclubDbProperties.DbSchema);
                    b.Property(x => x.Street).HasMaxLength(AddressConsts.StreetMaxLength);
                    b.Property(x => x.Number).HasMaxLength(AddressConsts.NumberMaxLength);
                    b.Property(x => x.PostalCode).HasMaxLength(AddressConsts.PostalCodeMaxLength);
                    b.Property(x => x.City).HasMaxLength(AddressConsts.CityMaxLength);
                });
        });

        builder.Entity<Division>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Divisions", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(DivisionConsts.NameMaxLength);
            b.HasOne(x => x.Season).WithMany(x => x.Divisions).HasForeignKey(x => x.SeasonId);
        });

        builder.Entity<Frame>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Frames", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Match).WithMany(x => x.Frames).HasForeignKey(x => x.MatchId);
            b.HasOne(x => x.HomePlayer).WithMany().HasForeignKey(x => x.HomePlayerId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.AwayPlayer).WithMany().HasForeignKey(x => x.AwayPlayerId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Match>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Matches", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.HomeTeam).WithMany().HasForeignKey(x => x.HomeTeamId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.AwayTeam).WithMany().HasForeignKey(x => x.AwayTeamId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MatchTeamPlayer>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "MatchTeamPlayers", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Match).WithMany(x => x.TeamPlayers).HasForeignKey(x => x.MatchId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Player>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Players", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.FirstName).IsRequired().HasMaxLength(PlayerConsts.FirstNameMaxLength);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(PlayerConsts.LastNameMaxLength);
            b.HasOne(x => x.Club).WithMany(x => x.Players).HasForeignKey(x => x.ClubId);
            b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        });

        builder.Entity<Season>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Seasons", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.StartDate).IsRequired();
            b.Property(x => x.EndDate).IsRequired();
        });

        builder.Entity<Team>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Teams", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(ClubConsts.NameMaxLength);
            b.HasOne(x => x.Club).WithMany(x => x.Teams).HasForeignKey(x => x.ClubId);
            b.HasOne(x => x.Division).WithMany(x => x.Teams).HasForeignKey(x => x.DivisionId);
        });

        builder.Entity<TeamPlayer>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "TeamPlayers", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Team).WithMany(x => x.Players).HasForeignKey(x => x.TeamId);
            b.HasOne(x => x.Player).WithMany().HasForeignKey(x => x.PlayerId);
        });
    }
}