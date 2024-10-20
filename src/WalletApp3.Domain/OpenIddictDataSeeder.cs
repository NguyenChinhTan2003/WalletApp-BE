using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.Uow;
using Volo.Abp;
using System.Diagnostics.CodeAnalysis;

namespace Wallet.App
{
    public class OpenIddictDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenIddictApplicationManager _applicationManager;

        public OpenIddictDataSeeder(
            IConfiguration configuration,
            IOpenIddictApplicationManager applicationManager)
        {
            _configuration = configuration;
            _applicationManager = applicationManager;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await CreateApplicationsAsync();
        }

        private async Task CreateApplicationsAsync()
        {
            var commonScopes = new List<string>
            {
                OpenIddictConstants.Permissions.Scopes.Address,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Phone,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles
            };

            var configurationSection = _configuration.GetSection("OpenIddict:Applications");

            // Existing App
            var clientId = configurationSection["AppFlutter:ClientId"];
            if (!clientId.IsNullOrWhiteSpace())
            {
                var client = await _applicationManager.FindByClientIdAsync(clientId);
                if (client == null)
                {
                    await CreateClientAsync(
                        name: clientId,
                        type: OpenIddictConstants.ClientTypes.Confidential,
                        consentType: OpenIddictConstants.ConsentTypes.Implicit,
                        displayName: "AppFlutter",
                        secret: "1q2w3e*",
                        grantTypes: new List<string>
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.GrantTypes.Password,
                            OpenIddictConstants.GrantTypes.RefreshToken
                        },
                        scopes: commonScopes,
                        redirectUri: null,
                        clientUri: null,
                        postLogoutRedirectUri: null
                    );
                }
            }
        }

        private async Task CreateClientAsync(
            [NotNull] string name,
            [NotNull] string type,
            [NotNull] string consentType,
            string displayName,
            string secret,
            List<string> grantTypes,
            List<string> scopes,
            string clientUri = null,
            string redirectUri = null,
            string postLogoutRedirectUri = null,
            List<string> permissions = null)
        {
            var client = await _applicationManager.FindByClientIdAsync(name);
            if (client == null)
            {
                var application = new OpenIddictApplicationDescriptor
                {
                    ClientId = name,
                    ClientSecret = secret,
                    ConsentType = consentType,
                    DisplayName = displayName,
                };

                Check.NotNullOrEmpty(grantTypes, nameof(grantTypes));
                Check.NotNullOrEmpty(scopes, nameof(scopes));

                if (grantTypes.Any())
                {
                    application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
                    application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                }

                foreach (var grantType in grantTypes)
                {
                    application.Permissions.Add(grantType);
                }

                foreach (var scope in scopes)
                {
                    application.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
                }

                await _applicationManager.CreateAsync(application);
            }
        }
    }
}
