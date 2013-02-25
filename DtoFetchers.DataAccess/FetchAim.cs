namespace DtoFetchers.DataAccess
{
    /// <summary>
    /// Цель извлечения DTO.
    /// </summary>
    public enum FetchAim
    {
        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        None,

        /// <summary>
        /// Карточка
        /// </summary>
        Card,

        /// <summary>
        /// Список
        /// </summary>
        List,

        /// <summary>
        /// Индекс
        /// </summary>
        Index
    }
}