using System;
using System.Linq;
using Common;
using DAL.Entities.Account;
using Enums;
using Microsoft.AspNet.Identity;

namespace DAL.Repositories {
    public class UserRepository : Repository<User> {
        public UserRepository(Repository repository)
            : base(repository) {
        }

        public UserRepository() {
        }

        public string[] GetPermissions(string userName) {
            User user = null;
            Role role = null;
            var permissions = this.Session.QueryOver<Permission>()
                .JoinQueryOver(x => x.Roles, () => role)
                .JoinAlias(() => role.Users, () => user)
                .Where(() => user.UserName == userName)
                .List();
            return permissions.Select(x => x.Name).ToArray();

        }

        public User GetByName(string userName) {
            return this.Session.QueryOver<User>().Where(x => x.UserName == userName).SingleOrDefault();
        }

        public void UpdateUserType() {
            try {
                var users = this.Get(x => x.UserType == UserTypeEnum.None);
                if (users.Any()) {
                    foreach (var user in users) {
                        var query = this.Session.CreateSQLQuery(string.Format(@" select top 1 MAX(pr.Enum) from _User us
  inner join PermissionsToUsers prus on (prus.UserId = us.Id)
  inner join _Permission pr on (pr.Id = prus.PermissionId)
  where us.UserName = '{0}'", user.UserName));
                        var result = query.UniqueResult<int>();
                        user.UserType = (UserTypeEnum)result;
                        this.Update(user);
                    }
                    this.Commit();
                    var r = this.Session.CreateSQLQuery("DROP TABLE PermissionsToUsers").UniqueResult();

                    var permissionRep = new Repository<Permission>(this);
                    var permissionIds = permissionRep.Get(x => x.Name == null).Select(x => x.Id).ToArray();
                    foreach (var permissionId in permissionIds) {
                        permissionRep.Delete(permissionId);
                    }
                    permissionRep.Commit();
                }
            } catch (Exception ex) {
                NHibLogger.Error("UpdateUserType", ex);
            }
        }

        //public User CreateNewUser(UserTypeEnum userType, Profile profile) {
        //    var user = this.Get(x => x.UserName == profile.IIN).SingleOrDefault();
        //    if (user != null) {
        //        return user;
        //    }
        //    user = new User() {
        //        UserType = userType,
        //        Profile = profile,
        //        UserName = profile.IIN,
        //        LastPasswordChangedDate = DateTime.Now,
        //        Password = new PasswordHasher().HashPassword(Constants.Applicant.ClientDefaultPassword),
        //    };
        //    this.Insert(user);
        //    return user;
        //}
    }
}