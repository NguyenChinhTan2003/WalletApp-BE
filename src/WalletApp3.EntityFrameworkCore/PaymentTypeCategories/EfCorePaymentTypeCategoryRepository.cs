using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WalletApp3.EntityFrameworkCore;

namespace WalletApp3.PaymentTypeCategories
{
    public abstract class EfCorePaymentTypeCategoryRepositoryBase : EfCoreRepository<WalletApp3DbContext, PaymentTypeCategory, int>
    {
        public EfCorePaymentTypeCategoryRepositoryBase(IDbContextProvider<WalletApp3DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        string? categoryName = null,
            bool? status = null,
            string? description = null,
            CancellationToken cancellationToken = default)
        {

            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, categoryName, status, description);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<PaymentTypeCategory>> GetListAsync(
            string? filterText = null,
            string? categoryName = null,
            bool? status = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, categoryName, status, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PaymentTypeCategoryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? categoryName = null,
            bool? status = null,
            string? description = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, categoryName, status, description);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PaymentTypeCategory> ApplyFilter(
            IQueryable<PaymentTypeCategory> query,
            string? filterText = null,
            string? categoryName = null,
            bool? status = null,
            string? description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CategoryName!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(categoryName), e => e.CategoryName.Contains(categoryName))
                    .WhereIf(status.HasValue, e => e.Status == status)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}