using System;

namespace DtoFetchers.Dto
{
    /// <summary>
    /// DTO космического метеорита.
    /// </summary>
    public class SpaceMeteorDto: MeteorDto
    {
        public DateTime DetectedAt { get; set; }

        public string DetectingPersonName { get; set; }

        public Guid DetectingPersonId { get; set; }

        public string PlaceOfOriginName { get; set; }

        public Guid? PlaceOfOriginId { get; set; }
    }
}