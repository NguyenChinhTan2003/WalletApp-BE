using System.Threading.Tasks;

namespace WalletApp3.Data;

public interface IWalletApp3DbSchemaMigrator
{
    Task MigrateAsync();
}
