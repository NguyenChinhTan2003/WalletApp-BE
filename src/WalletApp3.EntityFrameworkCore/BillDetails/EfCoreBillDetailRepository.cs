using Volo.Abp.Identity;
using WalletApp3.PaymentTypeCategories;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Polly;
using System.Net.WebSockets;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Users;
using Microsoft.Extensions.Logging;

namespace WalletApp3.BillDetails
{
    public abstract class EfCoreBillDetailRepositoryBase : EfCoreRepository<WalletApp3DbContext, BillDetail, int>
    {
        private readonly ICurrentUser _currentUser;
        public EfCoreBillDetailRepositoryBase(IDbContextProvider<WalletApp3DbContext> dbContextProvider , ICurrentUser currentUser)
            : base(dbContextProvider)
        {
            _currentUser = currentUser;
        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
                        decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            int? categoryId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();

            query = ApplyFilter(query, filterText, moneyMin, moneyMax, note, createdAtMin, createdAtMax, categoryId, userId);

            var ids = query.Select(x => x.BillDetail.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<BillDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(int id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(billDetail => new BillDetailWithNavigationProperties
                {
                    BillDetail = billDetail,
                    PaymentTypeCategory = dbContext.Set<PaymentTypeCategory>().FirstOrDefault(c => c.Id == billDetail.CategoryId),
                    IdentityUser = dbContext.Set<IdentityUser>().FirstOrDefault(c => c.Id == billDetail.UserId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<BillDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, moneyMin, moneyMax, note, createdAtMin, createdAtMax, categoryId, userId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BillDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<BillDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from billDetail in (await GetDbSetAsync())
                   join paymentTypeCategory in (await GetDbContextAsync()).Set<PaymentTypeCategory>() on billDetail.CategoryId equals paymentTypeCategory.Id into paymentTypeCategories
                   from paymentTypeCategory in paymentTypeCategories.DefaultIfEmpty()
                   join identityUser in (await GetDbContextAsync()).Set<IdentityUser>() on billDetail.UserId equals identityUser.Id into identityUsers
                   from identityUser in identityUsers.DefaultIfEmpty()
                   select new BillDetailWithNavigationProperties
                   {
                       BillDetail = billDetail,
                       PaymentTypeCategory = paymentTypeCategory,
                       IdentityUser = identityUser
                   };
        }

        protected virtual IQueryable<BillDetailWithNavigationProperties> ApplyFilter(
            IQueryable<BillDetailWithNavigationProperties> query,
            string? filterText,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            int? categoryId = null,
            Guid? userId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BillDetail.Note!.Contains(filterText!))
                    .WhereIf(moneyMin.HasValue, e => e.BillDetail.Money >= moneyMin!.Value)
                    .WhereIf(moneyMax.HasValue, e => e.BillDetail.Money <= moneyMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.BillDetail.Note.Contains(note))
                    .WhereIf(createdAtMin.HasValue, e => e.BillDetail.CreatedAt >= createdAtMin!.Value)
                    .WhereIf(createdAtMax.HasValue, e => e.BillDetail.CreatedAt <= createdAtMax!.Value)
                    .WhereIf(categoryId != null, e => e.PaymentTypeCategory != null && e.PaymentTypeCategory.Id == categoryId)
                    .WhereIf(userId != null && userId != Guid.Empty, e => e.IdentityUser != null && e.IdentityUser.Id == userId);
        }

        public virtual async Task<List<BillDetail>> GetListAsync(
            string? filterText = null,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, moneyMin, moneyMax, note, createdAtMin, createdAtMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BillDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }


        public virtual async Task<TotalBillDetailViewModel> GetBalance()
        {

            TotalBillDetailViewModel result = new TotalBillDetailViewModel();
            try
            {
                Guid? userId = _currentUser.Id;
                var context = await GetDbContextAsync();
                //lay danh sach danh muc loai thanh toan
                var catagorys = await context.PaymentTypeCategories.ToListAsync();

                //kiem tra co data danh muc
                if (catagorys != null && catagorys.Count > 0)
                {

                    var expensesCatagory = catagorys.Where(f => f.Status == true).Select(f => f.Id).ToList();
                    //lay tong tin chi = sum tong tien trong bill co loai thanh toan status = false
                    result.TotalExpenses = await context.BillDetails.AsNoTracking().Where(x => x.UserId == userId
                    && expensesCatagory.Contains(x.CategoryId)).SumAsync(x => x.Money);

                    var incomeCatagory = catagorys.Where(f => f.Status == false).Select(f => f.Id).ToList();
                    //lay tong tin thu = sum tong tien trong bill co loai thanh toan status = true
                    result.TotalIncome = await context.BillDetails.AsNoTracking().Where(x => x.UserId == userId
                    && incomeCatagory.Contains(x.CategoryId)).SumAsync(x => x.Money);

                    result.TotalBalanace = result.TotalIncome - result.TotalExpenses;

                }
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }


        public virtual async Task<List<TotalCategoryDateViewModel>> GetTotalCategory()
        {
            List<TotalCategoryDateViewModel> result = new List<TotalCategoryDateViewModel>();

            try
            {
                Guid? userId = _currentUser.Id;
                var context = await GetDbContextAsync();
                var query = from p in context.PaymentTypeCategories
                            join b in context.BillDetails on p.Id equals b.CategoryId
                            where b.UserId == userId
                            group b by new { p.Id, p.CategoryName, b.CreatedAt.Date } into g
                            select new TotalCategoryDateViewModel
                            {
                                CategoryId = g.Key.Id,
                                CategoryName = g.Key.CategoryName,
                                CreatedAt = g.Key.Date,
                                Money = g.Sum(x => x.Money)
                            };
                result = await query.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public virtual async Task<List<TotalCategoryViewModel>> GetCategory(bool status)
        {
            List<TotalCategoryViewModel> result = new List<TotalCategoryViewModel>();

            try
            {
                Guid? userId = _currentUser.Id;
                var context = await GetDbContextAsync();
                var query = from p in context.PaymentTypeCategories
                            join b in context.BillDetails on p.Id equals b.CategoryId
                            where b.UserId == userId && p.Status == status
                            group b by new { p.Id, p.CategoryName } into g
                            select new TotalCategoryViewModel
                            {
                                CategoryId = g.Key.Id,
                                CategoryName = g.Key.CategoryName,
                                Money = g.Sum(x => x.Money)
                            };
                result = await query.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public virtual async Task<List<TotalToDayViewModel>> GetTotalToDay(bool status )
        {
            List<TotalToDayViewModel> result = new List<TotalToDayViewModel>();
            try
            {
                Guid? userId = _currentUser.Id;
                var context = await GetDbContextAsync();
                //lay danh sach danh muc loai thanh toan
                var category = await context.BillDetails.ToListAsync();
                
                //kiem tra co data danh muc
                if (category != null && category.Count > 0)
                {

                    var query = from p in context.PaymentTypeCategories 
                                join b in context.BillDetails on p.Id equals b.CategoryId
                                where b.UserId == userId && p.Status == status
                                group b by new { b.CreatedAt } into g
                                select new TotalToDayViewModel
                                {
                                    Date = g.Key.CreatedAt,
                                    Total = g.Sum(x => x.Money)
                                };
                    result = await query.ToListAsync();

                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public virtual async Task<List<TotalToDateViewModel>> GetTotalTodate(bool status, DateTime fromDate, DateTime toDate)
        {
            List<TotalToDateViewModel> result = new List<TotalToDateViewModel>();
            try
            {
                Guid? userId = _currentUser.Id;
                var context = await GetDbContextAsync();

                // Kiểm tra xem có dữ liệu BillDetails không
                if (await context.BillDetails.AnyAsync())
                {
                    var query = from p in context.PaymentTypeCategories
                                join b in context.BillDetails on p.Id equals b.CategoryId
                                where b.UserId == userId && p.Status == status && b.CreatedAt >= fromDate && b.CreatedAt <= toDate
                                group b by 1 into g
                                select new TotalToDateViewModel
                                {
                                    FromDate = fromDate,
                                    ToDate = toDate,
                                    Total = g.Sum(x => x.Money)
                                };

                    var totalResult = await query.FirstOrDefaultAsync();
                    if (totalResult != null)
                    {
                        result.Add(totalResult);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }


        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            int? categoryId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, moneyMin, moneyMax, note, createdAtMin, createdAtMax, categoryId, userId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<BillDetail> ApplyFilter(
            IQueryable<BillDetail> query,
            string? filterText = null,
            decimal? moneyMin = null,
            decimal? moneyMax = null,
            string? note = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Note!.Contains(filterText!))
                    .WhereIf(moneyMin.HasValue, e => e.Money >= moneyMin!.Value)
                    .WhereIf(moneyMax.HasValue, e => e.Money <= moneyMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note))
                    .WhereIf(createdAtMin.HasValue, e => e.CreatedAt >= createdAtMin!.Value)
                    .WhereIf(createdAtMax.HasValue, e => e.CreatedAt <= createdAtMax!.Value);
        }
    }
}