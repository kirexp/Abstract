using DAL.Entities.Account;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace DAL.Override.Account {
    public class ExternalTokenOverride : IAutoMappingOverride<ExternalToken> {
        public void Override(AutoMapping<ExternalToken> mapping) {
            mapping.Table("_ExternalToken");
        }
    }
}