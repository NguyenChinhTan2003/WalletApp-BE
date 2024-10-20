using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class PaymentTypeCategoryDtoBase : FullAuditedEntityDto<int>, IHasConcurrencyStamp
    {
        public string CategoryName { get; set; } = null!;
        public bool Status { get; set; }
        public string Description { get; set; } = null!;
        public DateTime LastModifiercationTime { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}