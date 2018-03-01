using FluentNHibernate.Conventions;

namespace DAL.Conventions
{
    internal class PluralizeTableNames : IClassConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            string typeName = instance.EntityType.Name;
            instance.Table(Inflector.Inflector.Pluralize(typeName));
        }
    }
}
