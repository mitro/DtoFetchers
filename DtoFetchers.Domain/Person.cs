namespace DtoFetchers.Domain
{
    /// <summary>
    /// Человек.
    /// </summary>
    public class Person: Entity
    {
        /// <summary>
        /// Полное имя.
        /// </summary>
        public string FullName { get; set; }
    }
}