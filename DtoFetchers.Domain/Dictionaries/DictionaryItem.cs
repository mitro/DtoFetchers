namespace DtoFetchers.Domain.Dictionaries
{
    /// <summary>
    /// Базовый класс элемента справочника.
    /// </summary>
    public class DictionaryItem: Entity
    {
        /// <summary>
        /// Наименование элемента справочника.
        /// </summary>
        public string Name { get; set; }
    }
}