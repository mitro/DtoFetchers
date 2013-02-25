using System.Linq;

namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Возвращает LINQ-запрос.
        /// </summary>
        /// <typeparam name="T">Тип сущности, по которой создается запрос</typeparam>
        /// <returns>LINQ-запрос</returns>
        IQueryable<T> Query<T>();
    }
}