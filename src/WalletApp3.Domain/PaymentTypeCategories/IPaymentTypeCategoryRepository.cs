using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WalletApp3.PaymentTypeCategories
{
    public partial interface IPaymentTypeCategoryRepository : IRepository<PaymentTypeCategory, int>
    {

        Task DeleteAllAsync(
            string? filterText = null,
            string? categoryName = null,
            bool? status = null,
            string? description = null,
            CancellationToken cancellationToken = default);
        Task<List<PaymentTypeCategory>> GetListAsync(
                    string? filterText = null,
                    string? categoryName = null,
                    bool? status = null,
                    string? description = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? categoryName = null,
            bool? status = null,
            string? description = null,
            CancellationToken cancellationToken = default);
    }
}