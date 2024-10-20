using System;

namespace WalletApp3.PaymentTypeCategories;

public abstract class PaymentTypeCategoryDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}