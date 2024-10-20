using Microsoft.AspNetCore.Builder;
using WalletApp3;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<WalletApp3WebTestModule>();

public partial class Program
{
}
