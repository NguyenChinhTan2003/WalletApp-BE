using WalletApp3.Samples;
using Xunit;

namespace WalletApp3.EntityFrameworkCore.Domains;

[Collection(WalletApp3TestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<WalletApp3EntityFrameworkCoreTestModule>
{

}
