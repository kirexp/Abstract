using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace DAL.Repositories {
    public abstract class Repository : IDisposable {
        protected internal ISession Session;
        public virtual void Dispose() {
            if (this.Session == null) {
                return;
            }
            this.TransactionRollBack();
            //TransactionCommit();
        }
        protected void TransactionCommit() {
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive) {
                if (!this.Session.Transaction.WasCommitted && !this.Session.Transaction.WasRolledBack) {
                    this.Session.Transaction.Commit();
                    this.Session.Transaction.Begin();
                }
                //Session.Transaction.Dispose();
            }
        }

        protected void TransactionRollBack()
        {
            if (this.Session.Transaction != null && this.Session.Transaction.IsActive)
            {
                if (!this.Session.Transaction.WasCommitted && !this.Session.Transaction.WasRolledBack)
                {
                    this.Session.Transaction.Rollback();
                    this.Session.Transaction.Begin();
                }
                this.Session.Transaction.Dispose();
            }
        }
        public bool Commit() {
            try {
                this.TransactionCommit();
            }
            catch (Exception ex) {
                NHibLogger.Error("Repository.Commit", ex);
                throw;
            }
            return true;
        }
        protected void OpenTransactionIfNotOpened() {
            if (this.Session.Transaction == null || !this.Session.Transaction.IsActive) {
                this.Session.BeginTransaction();
            }
        }
        public ISession GetCurrentSession { get { return this.Session; } }
    }

    public class Repository<T> : Repository where T : class ,IEntity {
        public Repository() {
            this.Session = Provider.GetCurrentSession() ?? Provider.OpenDbSession();
            this.Session.FlushMode = FlushMode.Always;
        }

        public Repository(Repository repository) {
            this.Session = repository.Session;
        }


        public virtual IQueryable<T> Get() {
            return this.Session.Query<T>();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate) {
            return this.Get().Where(predicate);
        }

        public virtual T Get(long id) {
            return this.Session.Get<T>(id);
        }

        public virtual TT Load<TT>(long id) {
            return this.Session.Load<TT>(id);
        }

        public virtual void Insert(T entity) {
            this.OpenTransactionIfNotOpened();
            this.Session.Save(entity);
        }

        public virtual void Insert(IEnumerable<T> entities) {
            this.OpenTransactionIfNotOpened();
            foreach (var entity in entities) {
                this.Session.Save(entity);
            }
        }

        public virtual void Update(T entity) {
            this.OpenTransactionIfNotOpened();
            this.Session.Update(entity);
        }

        public virtual void InsertOrUpdate(IEnumerable<T> entities) {
            this.OpenTransactionIfNotOpened();
            foreach (var entity in entities) {
                this.Session.SaveOrUpdate(entity);
            }
        }

        public virtual void InsertOrUpdate(T entity) {
            this.OpenTransactionIfNotOpened();
            this.Session.SaveOrUpdate(entity);
        }

        public virtual void Delete(T entity) {
            this.OpenTransactionIfNotOpened();
            this.Session.Delete(entity);
        }

        public virtual void Delete(long id) {
            this.OpenTransactionIfNotOpened();
            this.Session.Delete(this.Session.Load<T>(id));
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return this.Session.Query<T>().Any();
            }
            return this.Session.Query<T>().Any(predicate);
        }
        public virtual int Count() {
            var count = this.Session.CreateCriteria<T>().SetProjection(Projections.RowCount()).UniqueResult<int>();
            return count;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return this.Session.QueryOver<T>().Where(predicate).RowCount();
        }


        public virtual void StorageProc(string nameOfProcedure,string[] parameters)
        {
            using (var sesstion = new Repository<T>().Session)
            {
                sesstion.GetNamedQuery(nameOfProcedure);
            }
        }

        //TODO думаю над реализацией пейджинга... (если уже не забыл)
        public virtual IQueryable<T> Page(ref int number, int size, out int count, Expression<Func<T, bool>> expression = null, bool orderByAsc = true) {
            
            var rowCount = expression != null ? this.Count(expression) : this.Count();

            count = rowCount / size;

            if (rowCount%size > 0) {
                count++;                
            }

            if (number < 1) {
                number = 1;                
            }

            if (number > count) {
                number = count;
            }

            var query = expression != null?  this.Get(expression): this.Get();
            query = orderByAsc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
            return query.Skip((number - 1) * size).Take(size);
        }
    }
}
