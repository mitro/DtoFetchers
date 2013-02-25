namespace DtoFetchers.Domain
{
    /// <summary>
    /// Секретный завод.
    /// </summary>
    public class SecretFactory: Entity
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Директор.
        /// </summary>
        public Person Director { get; set; }
    }
}