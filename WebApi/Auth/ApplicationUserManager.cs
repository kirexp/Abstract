using System;
using System.Collections.Generic;
using DAL.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi.Auth {
    /// <summary>
    /// adsasd
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationUserManager<T>:Microsoft.AspNetCore.Identity.UserManager<User>
    {
        public ApplicationUserManager(Microsoft.AspNetCore.Identity.IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services,
            ILogger<Microsoft.AspNetCore.Identity.UserManager<User>> logger) : base(store, optionsAccessor,
            passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) {
           
        }
    }
}