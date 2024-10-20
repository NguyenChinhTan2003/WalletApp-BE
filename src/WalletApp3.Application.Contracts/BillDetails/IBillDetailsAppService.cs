using WalletApp3.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using WalletApp3.Shared;

namespace WalletApp3.BillDetails
{
    public partial interface IBillDetailsAppService : IApplicationService
    {

        Task<PagedResultDto<BillDetailWithNavigationPropertiesDto>> GetListAsync(GetBillDetailsInput input);

        Task<BillDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(int id);

        Task<BillDetailDto> GetAsync(int id);

        Task<PagedResultDto<LookupDto<int>>> GetPaymentTypeCategoryLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(int id);

        Task<BillDetailDto> CreateAsync(BillDetailCreateDto input);

        Task<BillDetailDto> UpdateAsync(int id, BillDetailUpdateDto input);

        Task DeleteByIdsAsync(List<int> billdetailIds);

        Task DeleteAllAsync(GetBillDetailsInput input);
        Task<WalletApp3.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
        Task<TotalBillDetailDto> GetBalance();
        Task<List<TotalCategoryDateDto>> GetTotalCategory();
        Task<List<TotalCategoryDto>> GetCategory(bool status);
        Task<List<TotalToDayDto>> GetTotalToDay(bool status);
        Task<List<TotalToDateDto>> GetTotalTodate(bool status, DateTime fromDate, DateTime toDate);


    }
}