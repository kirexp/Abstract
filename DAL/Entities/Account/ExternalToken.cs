using System;

namespace DAL.Entities.Account {
    public class ExternalToken : IEntity {
        public virtual long Id { get; set; }
        public virtual string Value { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Expiration { get; set; }
        public virtual User User { get; set; }

    }
}