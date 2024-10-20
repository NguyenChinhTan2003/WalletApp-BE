using WalletApp3.Localization;
using Volo.Abp.Application.Services;

namespace WalletApp3;

/* Inherit your application services from this class.
 */
public abstract class WalletApp3AppService : ApplicationService
{
    protected WalletApp3AppService()
    {
        LocalizationResource = typeof(WalletApp3Resource);
    }
}
