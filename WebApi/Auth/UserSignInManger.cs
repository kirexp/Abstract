using DAL.Entities.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi.Auth
{
    public class ApplicationSignInManger<TUser> : Microsoft.AspNetCore.Identity.SignInManager<User> where TUser : class{
        public ApplicationSignInManger(Microsoft.AspNetCore.Identity.UserManager<User> userManager,
            IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
            logger, schemes) {
        }
    }
}
