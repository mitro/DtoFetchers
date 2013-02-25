using NHibernate;

namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// NHibernate-реализация единицы работы. Обертка над ISession.
    /// </summary>
    public class NhUnitOfWork: IUnitOfWork
    {
        // Сессия NHibernate
        private readonly ISession _session;

        public NhUnitOfWork(ISession session)
        {
            _session = session;
        }

        public object GetSession()
        {
            return _session;
        }
    }
}