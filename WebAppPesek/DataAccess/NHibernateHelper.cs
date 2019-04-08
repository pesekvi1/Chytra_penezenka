using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace DataAccess
{
    public class NHibernateHelper
    {
        private static ISessionFactory _factory = null;

        public static ISession Session
        {
            get
            {
                if (_factory == null)
                {
                    /*var cfg = new Configuration();
                    _factory = cfg.Configure(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hibernate.cfg.xml"))
                     .BuildSessionFactory();
                     */

                    _factory = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Data Source=DESKTOP-A5PR65K;Initial Catalog=tnpw2;User Id=sa;Password=heslo1234;"))
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Vozidlo>())
                        .BuildSessionFactory();
                }



                return _factory.OpenSession();
            }
        }
    }
}
