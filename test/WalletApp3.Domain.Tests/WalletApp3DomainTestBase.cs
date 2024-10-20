using Volo.Abp.Modularity;

namespace WalletApp3;

/* Inherit from this class for your domain layer tests. */
public abstract class WalletApp3DomainTestBase<TStartupModule> : WalletApp3TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
