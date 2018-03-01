using System.Collections.Generic;

namespace DAL.Entities.Account {
    public class Permission : IEntity {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameRu { get; set; }
        public virtual string NameEn { get; set; }
        
        public virtual ISet<Role> Roles { get; set; }

        public Permission() {
            this.Roles = new HashSet<Role>();
        }
    }
}