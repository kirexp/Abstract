using System.Configuration;
using System.Linq;
using Common;
using DAL.Conventions;
using DAL.Entities.Account;
using DAL.Override.Account;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace DAL {
    public static class Provider {
        private static ISessionFactory _factory;
        private static readonly object SyncObject = new object();

        public static ISession OpenDbSession() {
            var session = GetSessionFactory().OpenSession();
            return session;
        }

        public static ISession GetCurrentSession() {
            var currentSession = GetSessionFactory().OpenSession();

            //            var currentSession = GetSessionFactory().GetCurrentSession();
            return currentSession;
        }

        private static ISessionFactory GetSessionFactory() {
            if (_factory == null) {
                lock (SyncObject) {
                    if (_factory == null) {
                        var connectionString = ConfigHelper.GetConnectionString("DefaultConnection");
                        var cfg = Fluently.Configure()
                            .Database(
                                MsSqlConfiguration.MsSql2008.ConnectionString(connectionString).FormatSql().ShowSql())
                            .Mappings(c =>
                                c.AutoMappings.Add(AutoMap.AssemblyOf<User>()
                                    .Where(x => x.GetInterfaces().Contains(typeof(IEntity)))
                                    .UseOverridesFromAssemblyOf<UserOverride>()
                                    .Conventions.Add<StringColumnLengthConvention>()
                                    .Conventions.Add<CustomForeignKeyConvention>()
                                    .Conventions.Add<EnumConvention>()))
                            .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true))
                            .BuildConfiguration();
                            _factory = cfg.BuildSessionFactory();                 
                    }
                }
            }
            return _factory;
        }

        public static ISession CurrentSessionContextUnbind() {
            var session = CurrentSessionContext.Unbind(GetSessionFactory());
            return session;
        }

        public static void CurrentSessionContextBind(ISession session) {
            CurrentSessionContext.Bind(session);
        }
    }
}
