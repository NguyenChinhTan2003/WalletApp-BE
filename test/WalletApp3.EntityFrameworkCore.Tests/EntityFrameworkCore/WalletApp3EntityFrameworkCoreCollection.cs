using Xunit;

namespace WalletApp3.EntityFrameworkCore;

[CollectionDefinition(WalletApp3TestConsts.CollectionDefinitionName)]
public class WalletApp3EntityFrameworkCoreCollection : ICollectionFixture<WalletApp3EntityFrameworkCoreFixture>
{

}
