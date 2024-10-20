using WalletApp3.PaymentTypeCategories;
using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace WalletApp3.BillDetails
{
    public abstract class BillDetailWithNavigationPropertiesBase
    {
        public BillDetail BillDetail { get; set; } = null!;

        public PaymentTypeCategory PaymentTypeCategory { get; set; } = null!;
        public IdentityUser IdentityUser { get; set; } = null!;
        

        
    }
}