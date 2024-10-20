using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using WalletApp3.Shared;

namespace WalletApp3.PaymentTypeCategories
{
    public partial interface IPaymentTypeCategoriesAppService : IApplicationService
    {

        Task<PagedResultDto<PaymentTypeCategoryDto>> GetListAsync(GetPaymentTypeCategoriesInput input);

        Task<PaymentTypeCategoryDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<PaymentTypeCategoryDto> CreateAsync(PaymentTypeCategoryCreateDto input);

        Task<PaymentTypeCategoryDto> UpdateAsync(int id, PaymentTypeCategoryUpdateDto input);
        Task DeleteByIdsAsync(List<int> paymenttypecategoryIds);

        Task DeleteAllAsync(GetPaymentTypeCategoriesInput input);
        Task<WalletApp3.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}