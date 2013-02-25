using System;

namespace DtoFetchers.Dto
{
    /// <summary>
    /// Базовый класс DTO метеорита.
    /// </summary>
    public abstract class MeteorDto: BaseDto
    {
        public string Name { get; set; }

        public double Weight { get; set; }

        public string MaterialName { get; set; }

        public Guid? MaterialId { get; set; }

        public double DistanceToEarth { get; set; }

        public string RiskLevelName { get; set; }

        public Guid RiskLevelId { get; set; }
    }
}