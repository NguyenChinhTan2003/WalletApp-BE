using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using WalletApp3.Localization;

namespace WalletApp3.Web;

[Dependency(ReplaceServices = true)]
public class WalletApp3BrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<WalletApp3Resource> _localizer;

    public WalletApp3BrandingProvider(IStringLocalizer<WalletApp3Resource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
