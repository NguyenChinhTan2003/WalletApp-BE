using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace WalletApp3.Pages;

[Collection(WalletApp3TestConsts.CollectionDefinitionName)]
public class Index_Tests : WalletApp3WebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
