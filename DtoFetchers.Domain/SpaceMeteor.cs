using System;
using DtoFetchers.Domain.Dictionaries;

namespace DtoFetchers.Domain
{
    /// <summary>
    /// Космический метеорит.
    /// </summary>
    public class SpaceMeteor: Meteor
    {
        /// <summary>
        /// Дата/время обнаружения.
        /// </summary>
        public DateTime DetectedAt { get; set; }

        /// <summary>
        /// Обнаруживший человек.
        /// </summary>
        public Person DetectingPerson { get; set; }

        /// <summary>
        /// Галактика, из которой прилетел.
        /// </summary>
        public Galaxy PlaceOfOrigin { get; set; }
    }
}