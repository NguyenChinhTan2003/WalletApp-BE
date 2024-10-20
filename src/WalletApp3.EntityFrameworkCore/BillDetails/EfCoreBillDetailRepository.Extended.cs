using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;
using WalletApp3.EntityFrameworkCore;

namespace WalletApp3.BillDetails
{
    public class EfCoreBillDetailRepository : EfCoreBillDetailRepositoryBase, IBillDetailRepository
    {
        public EfCoreBillDetailRepository(IDbContextProvider<WalletApp3DbContext> dbContextProvider,ICurrentUser currentUser)
            : base(dbContextProvider, currentUser)
        {

        }

   
    }
}