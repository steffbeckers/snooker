using Microsoft.EntityFrameworkCore;
using Volo.Abp;

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
        }
    }
}