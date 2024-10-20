using WalletApp3.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace WalletApp3.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WalletApp3EntityFrameworkCoreModule),
    typeof(WalletApp3ApplicationContractsModule)
)]
public class WalletApp3DbMigratorModule : AbpModule
{
}
