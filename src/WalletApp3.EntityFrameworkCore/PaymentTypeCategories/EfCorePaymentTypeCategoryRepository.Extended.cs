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
    public class EfCorePaymentTypeCategoryRepository : EfCorePaymentTypeCategoryRepositoryBase, IPaymentTypeCategoryRepository
    {
        public EfCorePaymentTypeCategoryRepository(IDbContextProvider<WalletApp3DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}