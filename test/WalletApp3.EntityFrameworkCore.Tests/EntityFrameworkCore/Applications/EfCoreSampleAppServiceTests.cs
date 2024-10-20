using WalletApp3.Samples;
using Xunit;

namespace WalletApp3.EntityFrameworkCore.Applications;

[Collection(WalletApp3TestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<WalletApp3EntityFrameworkCoreTestModule>
{

}
