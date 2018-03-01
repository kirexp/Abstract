using DAL.Entities.Account;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace DAL.Override.Account {
    public class RoleOverride : IAutoMappingOverride<Role> {
        public void Override(AutoMapping<Role> mapping) {
            mapping.HasManyToMany(x => x.Permissions).Cascade.All();
        }
    }
}