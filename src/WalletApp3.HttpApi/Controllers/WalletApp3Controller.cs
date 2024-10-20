using WalletApp3.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WalletApp3.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WalletApp3Controller : AbpControllerBase
{
    protected WalletApp3Controller()
    {
        LocalizationResource = typeof(WalletApp3Resource);
    }
}
