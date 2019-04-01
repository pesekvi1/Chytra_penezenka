using System;
using DataAccess;
using DataAccess.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenerateSchema()
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    "Data Source=DESKTOP-A5PR65K;Initial Catalog=tnpw2;User Id=sa;Password=heslo1234;"))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<PersonMap>()).BuildConfiguration();

            new SchemaExport(configuration).Execute(false, true, false);
        }

        [TestMethod]
        public void UpdateSchema()
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    "Data Source=DESKTOP-A5PR65K;Initial Catalog=tnpw2;User Id=sa;Password=heslo1234;"))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<PersonMap>()).BuildConfiguration();

            new SchemaUpdate(configuration).Execute(false, true);

        }

        [TestMethod]
        public void MigrateSchema()
        {
            FluentNhibernateMigration.MigrateSchema();
        }
    }
}
