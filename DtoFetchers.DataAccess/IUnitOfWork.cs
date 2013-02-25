namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// Единица работы.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Возвращает конкретную реализацию.
        /// </summary>
        object GetSession();
    }
}