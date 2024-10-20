using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WalletApp3.Data;
using Volo.Abp.DependencyInjection;

namespace WalletApp3.EntityFrameworkCore;

public class EntityFrameworkCoreWalletApp3DbSchemaMigrator
    : IWalletApp3DbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWalletApp3DbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the WalletApp3DbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WalletApp3DbContext>()
            .Database
            .MigrateAsync();
    }
}
