namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// Фабрика единиц работы.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Возвращает новую единицу работы.
        /// </summary>
        IUnitOfWork Create();
    }
}