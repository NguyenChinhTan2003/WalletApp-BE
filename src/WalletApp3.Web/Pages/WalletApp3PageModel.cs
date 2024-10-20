using WalletApp3.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WalletApp3.Web.Pages;

public abstract class WalletApp3PageModel : AbpPageModel
{
    protected WalletApp3PageModel()
    {
        LocalizationResourceType = typeof(WalletApp3Resource);
    }
}
