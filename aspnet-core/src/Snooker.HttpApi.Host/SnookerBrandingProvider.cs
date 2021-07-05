using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Snooker
{
    [Dependency(ReplaceServices = true)]
    public class SnookerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Snooker";
    }
}