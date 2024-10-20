using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WalletApp3.Data;

/* This is used if database provider does't define
 * IWalletApp3DbSchemaMigrator implementation.
 */
public class NullWalletApp3DbSchemaMigrator : IWalletApp3DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
