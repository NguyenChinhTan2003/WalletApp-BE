using Volo.Abp.Modularity;

namespace WalletApp3;

public abstract class WalletApp3ApplicationTestBase<TStartupModule> : WalletApp3TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
