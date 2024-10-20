using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WalletApp3.Permissions;
using WalletApp3.PaymentTypeCategories;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using WalletApp3.Shared;
using WalletApp3;

namespace WalletApp3.PaymentTypeCategories
{

    [Authorize(AppPermissions.PaymentTypeCategories.Default)]
    public abstract class PaymentTypeCategoriesAppServiceBase : WalletApp3AppService
    {
        protected IDistributedCache<PaymentTypeCategoryDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IPaymentTypeCategoryRepository _paymentTypeCategoryRepository;
        protected PaymentTypeCategoryManager _paymentTypeCategoryManager;

        public PaymentTypeCategoriesAppServiceBase(IPaymentTypeCategoryRepository paymentTypeCategoryRepository, PaymentTypeCategoryManager paymentTypeCategoryManager, IDistributedCache<PaymentTypeCategoryDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _paymentTypeCategoryRepository = paymentTypeCategoryRepository;
            _paymentTypeCategoryManager = paymentTypeCategoryManager;

        }

        public virtual async Task<PagedResultDto<PaymentTypeCategoryDto>> GetListAsync(GetPaymentTypeCategoriesInput input)
        {
            var totalCount = await _paymentTypeCategoryRepository.GetCountAsync(input.FilterText, input.CategoryName, input.Status, input.Description);
            var items = await _paymentTypeCategoryRepository.GetListAsync(input.FilterText, input.CategoryName, input.Status, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PaymentTypeCategoryDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PaymentTypeCategory>, List<PaymentTypeCategoryDto>>(items)
            };
        }

        public virtual async Task<PaymentTypeCategoryDto> GetAsync(int id)
        {
            return ObjectMapper.Map<PaymentTypeCategory, PaymentTypeCategoryDto>(await _paymentTypeCategoryRepository.GetAsync(id));
        }

        [Authorize(AppPermissions.PaymentTypeCategories.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _paymentTypeCategoryRepository.DeleteAsync(id);
        }

        [Authorize(AppPermissions.PaymentTypeCategories.Create)]
        public virtual async Task<PaymentTypeCategoryDto> CreateAsync(PaymentTypeCategoryCreateDto input)
        {

            var paymentTypeCategory = await _paymentTypeCategoryManager.CreateAsync(
            input.CategoryName, input.Status, input.Description, input.LastModifiercationTime
            );

            return ObjectMapper.Map<PaymentTypeCategory, PaymentTypeCategoryDto>(paymentTypeCategory);
        }

        [Authorize(AppPermissions.PaymentTypeCategories.Edit)]
        public virtual async Task<PaymentTypeCategoryDto> UpdateAsync(int id, PaymentTypeCategoryUpdateDto input)
        {

            var paymentTypeCategory = await _paymentTypeCategoryManager.UpdateAsync(
            id,
            input.CategoryName, input.Status, input.Description, input.LastModifiercationTime, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PaymentTypeCategory, PaymentTypeCategoryDto>(paymentTypeCategory);
        }


        [Authorize(AppPermissions.PaymentTypeCategories.Delete)]
        public virtual async Task DeleteByIdsAsync(List<int> paymenttypecategoryIds)
        {
            await _paymentTypeCategoryRepository.DeleteManyAsync(paymenttypecategoryIds);
        }

        [Authorize(AppPermissions.PaymentTypeCategories.Delete)]
        public virtual async Task DeleteAllAsync(GetPaymentTypeCategoriesInput input)
        {
            await _paymentTypeCategoryRepository.DeleteAllAsync(input.FilterText, input.CategoryName, input.Status, input.Description);
        }
        public virtual async Task<WalletApp3.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new PaymentTypeCategoryDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new WalletApp3.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}