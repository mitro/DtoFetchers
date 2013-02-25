using System.Linq;

namespace DtoFetchers.DataAccess.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Устанавливает страницу для запроса.
        /// </summary>
        /// <typeparam name="T">Тип объекта запроса</typeparam>
        /// <param name="query">Запрос</param>
        /// <param name="page">Информация о странице</param>
        /// <returns>Запрос с фильтрацией по странице</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, Page page)
        {
            if (page == DataAccess.Page.All) return query;

            return query.Skip(page.FirstItemNum).Take(page.Count);
        }
    }
}