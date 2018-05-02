using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Redoak.Domain.Service
{
    public class RedoakSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        public RedoakSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager,
            contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userId, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            SignInManager<TUser> signInManager = this;
           
            var byIdAsync = await signInManager.UserManager.FindByIdAsync(userId);
            if (byIdAsync == null)
                return SignInResult.Failed;
            return await signInManager.PasswordSignInAsync(byIdAsync, password, isPersistent, lockoutOnFailure);
        }
    }
}