using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Snooker.Interclub.Addresses;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Matches;
using Snooker.Interclub.Players;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
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

        builder.Entity<Break>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Breaks", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Frame).WithMany(x => x.Breaks).HasForeignKey(x => x.FrameId);
            b.HasOne(x => x.Player).WithMany().HasForeignKey(x => x.PlayerId);
            b.Property(x => x.Value).IsRequired();
        });

        builder.Entity<Division>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Divisions", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(DivisionConsts.NameMaxLength);
            b.HasOne(x => x.Season).WithMany(x => x.Divisions).HasForeignKey(x => x.SeasonId);

            ValueConverter<IList<DayOfWeek>, string> daysOfWeekConverter = new ValueConverter<IList<DayOfWeek>, string>(
                x => string.Join(',', x),
                x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Enum.Parse<DayOfWeek>(x))
                    .ToList());

            ValueComparer<IList<DayOfWeek>> daysOfWeekComparer = new ValueComparer<IList<DayOfWeek>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            b.Property(x => x.DaysOfWeek)
                .HasConversion(daysOfWeekConverter)
                .Metadata.SetValueComparer(daysOfWeekComparer);

            b.Property(x => x.RoundsDuringSeason).HasDefaultValue(2);
        });

        builder.Entity<Frame>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Frames", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Match).WithMany(x => x.Frames).HasForeignKey(x => x.MatchId);
            b.HasOne(x => x.HomePlayer).WithMany().HasForeignKey(x => x.HomePlayerId);
            b.HasOne(x => x.AwayPlayer).WithMany().HasForeignKey(x => x.AwayPlayerId);
        });

        builder.Entity<Match>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "Matches", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Division).WithMany(x => x.Matches).HasForeignKey(x => x.DivisionId);
            b.HasOne(x => x.HomeTeam).WithMany().HasForeignKey(x => x.HomeTeamId);
            b.HasOne(x => x.AwayTeam).WithMany().HasForeignKey(x => x.AwayTeamId);
        });

        builder.Entity<MatchTeamPlayer>(b =>
        {
            b.ToTable(InterclubDbProperties.DbTablePrefix + "MatchTeamPlayers", InterclubDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Match).WithMany(x => x.TeamPlayers).HasForeignKey(x => x.MatchId);
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