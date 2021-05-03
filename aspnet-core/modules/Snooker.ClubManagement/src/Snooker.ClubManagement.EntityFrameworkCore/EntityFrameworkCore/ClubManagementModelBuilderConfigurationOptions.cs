using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Snooker.ClubManagement.EntityFrameworkCore
{
    public class ClubManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ClubManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}