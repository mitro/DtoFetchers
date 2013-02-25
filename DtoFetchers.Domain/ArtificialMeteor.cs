using DtoFetchers.Domain.Dictionaries;

namespace DtoFetchers.Domain
{
    /// <summary>
    /// Метеорит неприродного происхождения.
    /// </summary>
    public class ArtificialMeteor: Meteor
    {
        /// <summary>
        /// Страна-изготовитель.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Завод-изготовитель.
        /// </summary>
        public SecretFactory Maker { get; set; }

        /// <summary>
        /// Заводской номер.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Контролер ОТК.
        /// </summary>
        public Person QualityEngineer { get; set; }
    }
}