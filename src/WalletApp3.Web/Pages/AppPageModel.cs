using WalletApp3.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WalletApp3.Web.Pages;

public abstract class AppPageModel : AbpPageModel
{
    protected AppPageModel()
    {
        LocalizationResourceType = typeof(WalletApp3Resource);
    }
}
