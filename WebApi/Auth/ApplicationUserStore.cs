using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities.Account;
using DAL.Repositories;
using Enums;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Auth {
    public class ApplicationUserStore : Microsoft.AspNetCore.Identity.IUserStore<User>, Microsoft.AspNetCore.Identity.IUserPasswordStore<User>,IUserRoleStore<User>, IUserClaimStore<User>
    {

        private readonly Repository<User> _repository;
        public Repository<User> GetCurrentRepository => this._repository;
        public ApplicationUserStore(SignInRepository rep) {
            this._repository = rep.Repository;
        }
        public void Dispose()
        {
            this._repository.Dispose();
        }
        public ApplicationUserStore(Repository<User> repository)
        {
            this._repository = repository;
        }
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) {
            return  Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) {
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) {
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken) {
            return await Task.Run(() => {
                this._repository.Insert(user);
                this._repository.Commit();    
                return IdentityResult.Success;
            });
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) {
            return Task.Run(() => {
                this._repository.Update(user);
                this._repository.Commit();
                return new IdentityResult();
            });
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) {
            return Task.Run(() => {
                this._repository.Delete(user);
                this._repository.Commit();
                return new IdentityResult();
            });
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            var id = Convert.ToInt32(userId);
            return Task.Run(() => this._repository.Get(x => x.Id == id).SingleOrDefault());
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            return Task.Run(() => this._repository.Get(x => x.UserName == normalizedUserName).SingleOrDefault());
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken) {
            user.Password = passwordHash;
            user.LastPasswordChangedDate = DateTime.Now;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult((IList<string>)user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            return Task.FromResult(user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name).Any(x => x == roleName));
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken) {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(nameof(UserTypeEnum), user.UserType.ToString()));
            return Task.FromResult(claims);
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}