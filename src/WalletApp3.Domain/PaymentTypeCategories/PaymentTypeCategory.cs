using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class PaymentTypeCategoryBase : FullAuditedAggregateRoot<int>
    {
        [NotNull]
        public virtual string CategoryName { get; set; }

        public virtual bool Status { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        public virtual DateTime LastModifiercationTime { get; set; }

        protected PaymentTypeCategoryBase()
        {

        }

        public PaymentTypeCategoryBase(string categoryName, bool status, string description, DateTime lastModifiercationTime)
        {

            Check.NotNull(categoryName, nameof(categoryName));
            Check.Length(categoryName, nameof(categoryName), PaymentTypeCategoryConsts.CategoryNameMaxLength, PaymentTypeCategoryConsts.CategoryNameMinLength);
            Check.NotNull(description, nameof(description));
            Check.Length(description, nameof(description), PaymentTypeCategoryConsts.DescriptionMaxLength, PaymentTypeCategoryConsts.DescriptionMinLength);
            CategoryName = categoryName;
            Status = status;
            Description = description;
            LastModifiercationTime = lastModifiercationTime;
        }

    }
}