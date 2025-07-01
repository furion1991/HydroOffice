using DotNetEnv;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HydroOffice.Database.Maps;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace HydroOffice.Database;

public static class DatabaseSessionHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory SessionFactory => _sessionFactory ??= BuildSessionFactory();

    private static ISessionFactory BuildSessionFactory()
    {

        var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                               $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                               $"Uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                               $"Pwd={Environment.GetEnvironmentVariable("DB_PASS")};";

        return Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings
                .AddFromAssemblyOf<EmployeeMap>()
                .AddFromAssemblyOf<ContractorMap>()
                .AddFromAssemblyOf<OrderMap>())
            .ExposeConfiguration(cfg =>
            {
                new SchemaUpdate(cfg).Execute(false, true);
            }).BuildSessionFactory();
    }
}