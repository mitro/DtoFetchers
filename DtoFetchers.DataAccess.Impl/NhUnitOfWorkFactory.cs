using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// NHibernate-реализация фабрики единиц работы.
    /// </summary>
    public class NhUnitOfWorkFactory : IUnitOfWorkFactory
    {
        // Фабрика сессий NHibernate
        private ISessionFactory _sessionFactory;

        private ISessionFactory SessionFactory
        {
            get
            {
                lock (this)
                {
                    if (_sessionFactory == null)
                    {
                        BuildSessionFactory();
                    }
                }

                return _sessionFactory;
            }
        }

        public IUnitOfWork Create()
        {
            return new NhUnitOfWork(SessionFactory.OpenSession());
        }

        private void BuildSessionFactory()
        {
            var config = new Configuration();

            config.Configure();

            config.AddAssembly(Assembly.GetExecutingAssembly());

            _sessionFactory = config.BuildSessionFactory();
        }
    }
}