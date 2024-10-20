using Volo.Abp.Modularity;

namespace WalletApp3;

[DependsOn(
    typeof(WalletApp3DomainModule),
    typeof(WalletApp3TestBaseModule)
)]
public class WalletApp3DomainTestModule : AbpModule
{

}
