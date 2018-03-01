using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace DAL.Conventions {
    public class EnumConvention : IUserTypeConvention {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) {
            criteria.Expect(x => x.Property.PropertyType.IsEnum || this.IsNullableEnum(x.Property.PropertyType));
        }

        public void Apply(IPropertyInstance target) {
            target.CustomType(target.Property.PropertyType);
        }

        public bool IsNullableEnum(Type t) {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
    }
}