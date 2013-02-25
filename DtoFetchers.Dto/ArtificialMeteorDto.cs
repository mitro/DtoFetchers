using System;

namespace DtoFetchers.Dto
{
    /// <summary>
    /// DTO метеорита неприродного происхождения.
    /// </summary>
    public class ArtificialMeteorDto: MeteorDto
    {
        public string CountryName { get; set; }

        public Guid CountryId { get; set; }

        public string MakerName { get; set; }

        public string MakerAddress { get; set; }

        public string MakerDirectorName { get; set; }

        public Guid MakerId { get; set; }

        public string SerialNumber { get; set; }

        public string QualityEngineerName { get; set; }

        public Guid QualityEngineerId { get; set; }
    }
}