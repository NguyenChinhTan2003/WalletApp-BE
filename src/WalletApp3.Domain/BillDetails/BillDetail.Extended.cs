using WalletApp3.PaymentTypeCategories;
using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace WalletApp3.BillDetails
{
    public class BillDetail : BillDetailBase
    {
        //<suite-custom-code-autogenerated>
        protected BillDetail()
        {

        }

        public BillDetail(int categoryId, Guid userId, decimal money, string note, DateTime createdAt)
            : base(categoryId, userId, money, note, createdAt)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}