using Volo.Abp.Modularity;

namespace WalletApp3;

[DependsOn(
    typeof(WalletApp3ApplicationModule),
    typeof(WalletApp3DomainTestModule)
)]
public class WalletApp3ApplicationTestModule : AbpModule
{

}
