using DtoFetchers.Domain.Dictionaries;

namespace DtoFetchers.Domain
{
    /// <summary>
    /// Базовый класс метеорита.
    /// </summary>
    public abstract class Meteor: Entity
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вес.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Из какого материала состоит метеорит.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// Расстояние до Земли.
        /// </summary>
        public double DistanceToEarth { get; set; }

        /// <summary>
        /// Уровень опасности метеорита.
        /// </summary>
        public RiskLevel RiskLevel { get; set; }
    }
}