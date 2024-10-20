using Volo.Abp.Settings;

namespace WalletApp3.Settings;

public class WalletApp3SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WalletApp3Settings.MySetting1));
    }
}
