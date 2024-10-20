using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Volo.Abp.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OpenIddict.Abstractions;
using Microsoft.Extensions.Options;

namespace WalletApp3.Users
{
    public class UserAuthenService : ApplicationService
    {
        
    }
}
