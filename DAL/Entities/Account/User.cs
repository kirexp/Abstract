using System;
using System.Collections.Generic;
using Enums;
using Microsoft.AspNet.Identity;

namespace DAL.Entities.Account
{
    public class User : IEntity, IUser<long> {
        public virtual long Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime LastPasswordChangedDate { get; set; }
        public virtual string Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual DateTimeOffset? LockoutEndDateUtc { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual UserTypeEnum UserType { get; set; }
        public virtual ISet<Role> Roles { get; set; }


        public User() {
            this.Roles = new HashSet<Role>();
        }
    }
}
