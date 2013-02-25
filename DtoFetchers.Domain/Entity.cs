using System;

namespace DtoFetchers.Domain
{
    /// <summary>
    /// Базовый класс всех сущностей.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }
    }
}