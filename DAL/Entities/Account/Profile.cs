//using System;

//namespace DAL.Entities.Account {
//    public class Profile : IEntity, ICloneable {
//        public virtual long Id { get; set; }
//        public virtual string IIN { get; set; }
//        public virtual string LastName { get; set; }
//        public virtual string FirstName { get; set; }
//        public virtual string MiddleName { get; set; }
//        public virtual DateTime BirthDate { get; set; }
//        public override string ToString() {
//            return string.Format("{0} {1} {2}", this.LastName, this.FirstName, this.MiddleName).Trim();
//        }
//        public virtual object Clone() {
//            var result = new Profile();
//            result = this.MemberwiseClone() as Profile;
//            return result;
//        }
//    }
//}
