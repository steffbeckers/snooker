using Microsoft.EntityFrameworkCore;
using Snooker.ClubPlayers;
using Snooker.Clubs;
using Snooker.Players;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace Snooker.EntityFrameworkCore
{
    public static class SnookerDbContextModelCreatingExtensions
    {
        public static void ConfigureSnooker(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(SnookerConsts.DbTablePrefix + "YourEntities", SnookerConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<ClubPlayer>(b =>
            {
                b.ToTable(SnookerConsts.DbTablePrefix + "ClubPlayers", SnookerConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.ClubId)
                    .HasColumnName(nameof(ClubPlayer.ClubId))
                    .IsRequired();
                b.Property(x => x.PlayerId)
                    .HasColumnName(nameof(ClubPlayer.PlayerId))
                    .IsRequired();

                b.HasOne<Club>()
                    .WithMany()
                    .HasForeignKey(x => x.ClubId);
                b.HasOne<Player>()
                    .WithMany()
                    .HasForeignKey(x => x.PlayerId);
            });

            builder.Entity<Club>(b =>
            {
                b.ToTable(SnookerConsts.DbTablePrefix + "Clubs", SnookerConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .HasColumnName(nameof(Club.Name))
                    .IsRequired()
                    .HasMaxLength(ClubConsts.NameMaxLength);
            });

            builder.Entity<Player>(b =>
            {
                b.ToTable(SnookerConsts.DbTablePrefix + "Players", SnookerConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.FirstName)
                    .HasColumnName(nameof(Player.FirstName))
                    .IsRequired()
                    .HasMaxLength(PlayerConsts.FirstNameMaxLength);
                b.Property(x => x.LastName)
                    .HasColumnName(nameof(Player.LastName))
                    .IsRequired()
                    .HasMaxLength(PlayerConsts.LastNameMaxLength);
                b.Property(x => x.UserId)
                    .HasColumnName(nameof(Player.UserId));

                b.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
            });
        }
    }
}