using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class PaymentTypeCategoryCreateDtoBase
    {
        [Required]
        [StringLength(PaymentTypeCategoryConsts.CategoryNameMaxLength, MinimumLength = PaymentTypeCategoryConsts.CategoryNameMinLength)]
        public string CategoryName { get; set; } = null!;
        public bool Status { get; set; } = true;
        [Required]
        [StringLength(PaymentTypeCategoryConsts.DescriptionMaxLength, MinimumLength = PaymentTypeCategoryConsts.DescriptionMinLength)]
        public string Description { get; set; } = null!;
        public DateTime LastModifiercationTime { get; set; }
    }
}