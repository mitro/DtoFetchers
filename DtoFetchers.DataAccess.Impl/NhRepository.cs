using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// NHibernate-реализация репозитория.
    /// </summary>
    public class NhRepository: IRepository
    {
        // Сессия NHibernate
        private readonly ISession _session;

        public NhRepository(IUnitOfWork uow)
        {
            _session = (ISession) uow.GetSession();
        }

        public IQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }
    }
}