using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class PaymentTypeCategoryManagerBase : DomainService
    {
        protected IPaymentTypeCategoryRepository _paymentTypeCategoryRepository;

        public PaymentTypeCategoryManagerBase(IPaymentTypeCategoryRepository paymentTypeCategoryRepository)
        {
            _paymentTypeCategoryRepository = paymentTypeCategoryRepository;
        }

        public virtual async Task<PaymentTypeCategory> CreateAsync(
        string categoryName, bool status, string description, DateTime lastModifiercationTime)
        {
            Check.NotNullOrWhiteSpace(categoryName, nameof(categoryName));
            Check.Length(categoryName, nameof(categoryName), PaymentTypeCategoryConsts.CategoryNameMaxLength, PaymentTypeCategoryConsts.CategoryNameMinLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), PaymentTypeCategoryConsts.DescriptionMaxLength, PaymentTypeCategoryConsts.DescriptionMinLength);
            Check.NotNull(lastModifiercationTime, nameof(lastModifiercationTime));

            var paymentTypeCategory = new PaymentTypeCategory(

             categoryName, status, description, lastModifiercationTime
             );

            return await _paymentTypeCategoryRepository.InsertAsync(paymentTypeCategory);
        }

        public virtual async Task<PaymentTypeCategory> UpdateAsync(
            int id,
            string categoryName, bool status, string description, DateTime lastModifiercationTime, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(categoryName, nameof(categoryName));
            Check.Length(categoryName, nameof(categoryName), PaymentTypeCategoryConsts.CategoryNameMaxLength, PaymentTypeCategoryConsts.CategoryNameMinLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), PaymentTypeCategoryConsts.DescriptionMaxLength, PaymentTypeCategoryConsts.DescriptionMinLength);
            Check.NotNull(lastModifiercationTime, nameof(lastModifiercationTime));

            var paymentTypeCategory = await _paymentTypeCategoryRepository.GetAsync(id);

            paymentTypeCategory.CategoryName = categoryName;
            paymentTypeCategory.Status = status;
            paymentTypeCategory.Description = description;
            paymentTypeCategory.LastModifiercationTime = lastModifiercationTime;

            paymentTypeCategory.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _paymentTypeCategoryRepository.UpdateAsync(paymentTypeCategory);
        }

    }
}