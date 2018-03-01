using DAL.Entities.Account;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace DAL.Override.Account {
    public class UserOverride:IAutoMappingOverride<User> {
        public void Override(AutoMapping<User> mapping) {
            mapping.Table("_User");
            mapping.HasManyToMany(x => x.Roles).Cascade.All();
            mapping.Map(x => x.UserType).Not.Nullable().Default("'0'");
            //mapping.HasOne(x => x.Profile).Cascade.All();
        }
    }
}