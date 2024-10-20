using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WalletApp3.BillDetails
{
    public partial interface IBillDetailRepository : IRepository<BillDetail, int>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            int? categoryId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);
        Task<BillDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
            int id,
            CancellationToken cancellationToken = default
        );

        Task<List<BillDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            int? categoryId = null,
            Guid? userId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<BillDetail>> GetListAsync(
                    string? filterText = null,
                    decimal? moneyMin = null,
                    decimal? moneyMax = null,
                    string? note = null,
                    DateTime? createdAtMin = null,
                    DateTime? createdAtMax = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            int? categoryId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);

        Task<TotalBillDetailViewModel> GetBalance();
        Task<List<TotalCategoryDateViewModel>> GetTotalCategory();
        Task<List<TotalCategoryViewModel>> GetCategory(bool status);
        Task<List<TotalToDayViewModel>> GetTotalToDay(bool status);
        Task<List<TotalToDateViewModel>> GetTotalTodate(bool status, DateTime fromDate, DateTime toDate);

    }
}