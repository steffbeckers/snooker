using Microsoft.EntityFrameworkCore;
using Snooker.Clubs;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

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

            builder.Entity<Club>(b =>
            {
                b.ToTable(SnookerConsts.DbTablePrefix + "Clubs", SnookerConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name)
                    .HasColumnName(nameof(Club.Name))
                    .IsRequired()
                    .HasMaxLength(ClubConsts.NameMaxLength);
            });
        }
    }
}