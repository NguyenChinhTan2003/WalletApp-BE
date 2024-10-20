using WalletApp3.Shared;
using Volo.Abp.Identity;
using WalletApp3.PaymentTypeCategories;
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
using WalletApp3.BillDetails;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using WalletApp3.Shared;
using Volo.Abp.ObjectMapping;
using System.Net.WebSockets;
using WalletApp3;

namespace WalletApp3.BillDetails
{

    [Authorize(AppPermissions.BillDetails.Default)]
    public abstract class BillDetailsAppServiceBase : WalletApp3AppService
    {
        protected IDistributedCache<BillDetailDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IBillDetailRepository _billDetailRepository;
        protected BillDetailManager _billDetailManager;

        protected IRepository<WalletApp3.PaymentTypeCategories.PaymentTypeCategory, int> _paymentTypeCategoryRepository;
        protected IRepository<Volo.Abp.Identity.IdentityUser, Guid> _identityUserRepository;

        public BillDetailsAppServiceBase(IBillDetailRepository billDetailRepository, BillDetailManager billDetailManager, IDistributedCache<BillDetailDownloadTokenCacheItem, string> downloadTokenCache, IRepository<WalletApp3.PaymentTypeCategories.PaymentTypeCategory, int> paymentTypeCategoryRepository, IRepository<Volo.Abp.Identity.IdentityUser, Guid> identityUserRepository)
        {
            _downloadTokenCache = downloadTokenCache;
            _billDetailRepository = billDetailRepository;
            _billDetailManager = billDetailManager; _paymentTypeCategoryRepository = paymentTypeCategoryRepository;
            _identityUserRepository = identityUserRepository;

        }

        public virtual async Task<PagedResultDto<BillDetailWithNavigationPropertiesDto>> GetListAsync(GetBillDetailsInput input)
        {
            var totalCount = await _billDetailRepository.GetCountAsync(input.FilterText, input.MoneyMin, input.MoneyMax, input.Note, input.CreatedAtMin, input.CreatedAtMax, input.CategoryId, input.UserId);
            var items = await _billDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.MoneyMin, input.MoneyMax, input.Note, input.CreatedAtMin, input.CreatedAtMax, input.CategoryId, input.UserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BillDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BillDetailWithNavigationProperties>, List<BillDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<BillDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(int id)
        {
            return ObjectMapper.Map<BillDetailWithNavigationProperties, BillDetailWithNavigationPropertiesDto>
                (await _billDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<BillDetailDto> GetAsync(int id)
        {
            return ObjectMapper.Map<BillDetail, BillDetailDto>(await _billDetailRepository.GetAsync(id));
        }


        [Authorize(AppPermissions.BillDetails.ViewTotalBalance)]
        public virtual async Task<TotalBillDetailDto> GetBalance()
        {
            return ObjectMapper.Map<TotalBillDetailViewModel, TotalBillDetailDto>(await _billDetailRepository.GetBalance());
        }

        [Authorize(AppPermissions.BillDetails.ViewTotalToDay)]
        public virtual async Task<List<TotalToDayDto>> GetTotalToDay(bool status)
        {
            var result = await _billDetailRepository.GetTotalToDay( status);
            return result.Select(x => ObjectMapper.Map<TotalToDayViewModel, TotalToDayDto>(x)).ToList();
        }

        [Authorize(AppPermissions.BillDetails.ViewTotalToDate)]
        public virtual async Task<List<TotalToDateDto>> GetTotalTodate(bool status, DateTime fromDate, DateTime toDate)
        {
            var result = await _billDetailRepository.GetTotalTodate(status, fromDate, toDate);
            return result.Select(x => ObjectMapper.Map<TotalToDateViewModel, TotalToDateDto>(x)).ToList();
        }

        [Authorize(AppPermissions.BillDetails.ViewTotalCategory)]
        public virtual async Task<List<TotalCategoryDateDto>> GetTotalCategory()
        {
            return (await _billDetailRepository.GetTotalCategory()).Select(x => ObjectMapper.Map<TotalCategoryDateViewModel, TotalCategoryDateDto>(x)).ToList();
        }

        [Authorize(AppPermissions.BillDetails.ViewCategory)]
        public virtual async Task<List<TotalCategoryDto>> GetCategory(bool status)
        {
            return (await _billDetailRepository.GetCategory(status)).Select(x => ObjectMapper.Map<TotalCategoryViewModel, TotalCategoryDto>(x)).ToList();
        }

        public virtual async Task<PagedResultDto<LookupDto<int>>> GetPaymentTypeCategoryLookupAsync(LookupRequestDto input)
        {
            var query = (await _paymentTypeCategoryRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.CategoryName != null &&
                         x.CategoryName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<WalletApp3.PaymentTypeCategories.PaymentTypeCategory>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<int>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WalletApp3.PaymentTypeCategories.PaymentTypeCategory>, List<LookupDto<int>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            var query = (await _identityUserRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Volo.Abp.Identity.IdentityUser>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(AppPermissions.BillDetails.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _billDetailRepository.DeleteAsync(id);
        }

        [Authorize(AppPermissions.BillDetails.Create)]
        public virtual async Task<BillDetailDto> CreateAsync(BillDetailCreateDto input)
        {
            if (input.CategoryId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PaymentTypeCategory"]]);
            }
            if (input.UserId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["IdentityUser"]]);
            }

            var billDetail = await _billDetailManager.CreateAsync(
            input.CategoryId, input.UserId, input.Money, input.Note, input.CreatedAt
            );

            return ObjectMapper.Map<BillDetail, BillDetailDto>(billDetail);
        }

        [Authorize(AppPermissions.BillDetails.Edit)]
        public virtual async Task<BillDetailDto> UpdateAsync(int id, BillDetailUpdateDto input)
        {
            if (input.CategoryId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PaymentTypeCategory"]]);
            }
            if (input.UserId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["IdentityUser"]]);
            }

            var billDetail = await _billDetailManager.UpdateAsync(
            id,
            input.CategoryId, input.UserId, input.Money, input.Note, input.CreatedAt, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<BillDetail, BillDetailDto>(billDetail);
        }

        [Authorize(AppPermissions.BillDetails.Delete)]
        public virtual async Task DeleteByIdsAsync(List<int> billdetailIds)
        {
            await _billDetailRepository.DeleteManyAsync(billdetailIds);
        }

        [Authorize(AppPermissions.BillDetails.Delete)]
        public virtual async Task DeleteAllAsync(GetBillDetailsInput input)
        {
            await _billDetailRepository.DeleteAllAsync(input.FilterText, input.MoneyMin, input.MoneyMax, input.Note, input.CreatedAtMin, input.CreatedAtMax, input.CategoryId, input.UserId);
        }
        public virtual async Task<WalletApp3.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new BillDetailDownloadTokenCacheItem { Token = token },
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