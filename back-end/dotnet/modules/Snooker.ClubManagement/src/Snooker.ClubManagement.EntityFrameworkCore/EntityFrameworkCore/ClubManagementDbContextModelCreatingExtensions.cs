using System;
using Microsoft.EntityFrameworkCore;
using Snooker.ClubManagement.Clubs;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Snooker.ClubManagement.EntityFrameworkCore
{
    public static class ClubManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureClubManagement(
            this ModelBuilder builder,
            Action<ClubManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ClubManagementModelBuilderConfigurationOptions(
                ClubManagementDbProperties.DbTablePrefix,
                ClubManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                // Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                // Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                // Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                // Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<Club>(b =>
            {
                // Configure table & schema name
                b.ToTable(options.TablePrefix + "Clubs", options.Schema);

                b.ConfigureByConvention();

                // Properties
                b.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(ClubConsts.NameMaxLength);

                // Indexes
                b.HasIndex(x => x.Name)
                    .IsUnique();
            });
        }
    }
}