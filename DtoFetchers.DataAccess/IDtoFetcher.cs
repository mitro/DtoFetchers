using System.Collections.Generic;
using System.Linq;
using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// Представляет механизм для извлечения DTO из хранилища.
    /// В зависимости от цели извлечения инициализируется разное подмножество свойств DTO.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущностей, находящихся в хранилище</typeparam>
    /// <typeparam name="TDto">Тип извлекаемых DTO</typeparam>
    public interface IDtoFetcher<in TEntity, out TDto>
        where TEntity : Entity
        where TDto : BaseDto
    {
        /// <summary>
        /// Извлекает список DTO из хранилища. Так как для разных целей
        /// нужны разные подмножества свойств DTO, указывается цель извлечения.
        /// Например, для отображения списка не заполняются коллекции и т.п.
        /// </summary>
        /// <param name="query">LINQ-запрос, содержащий критерии извлечения</param>
        /// <param name="page">Информация о странице</param>
        /// <param name="fetchAim">Цель извлечения</param>
        /// <returns>Список DTO</returns>
        IEnumerable<TDto> Fetch(IQueryable<TEntity> query, Page page, FetchAim fetchAim);
    }
}