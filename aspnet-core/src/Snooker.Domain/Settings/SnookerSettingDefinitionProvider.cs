using Volo.Abp.Settings;

namespace Snooker.Settings
{
    public class SnookerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(SnookerSettings.MySetting1));
        }
    }
}
