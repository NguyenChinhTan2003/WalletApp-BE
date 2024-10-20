using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class PaymentTypeCategoryUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(PaymentTypeCategoryConsts.CategoryNameMaxLength, MinimumLength = PaymentTypeCategoryConsts.CategoryNameMinLength)]
        public string CategoryName { get; set; } = null!;
        public bool Status { get; set; }
        [Required]
        [StringLength(PaymentTypeCategoryConsts.DescriptionMaxLength, MinimumLength = PaymentTypeCategoryConsts.DescriptionMinLength)]
        public string Description { get; set; } = null!;
        public DateTime LastModifiercationTime { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}