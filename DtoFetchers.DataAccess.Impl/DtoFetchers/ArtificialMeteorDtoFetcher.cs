using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers.DataAccess.DtoFetchers
{
    /// <summary>
    /// Фетчер DTO метеоритов неприродного происхождения.
    /// </summary>
    public class ArtificialMeteorDtoFetcher: BaseMeteorDtoFetcher<ArtificialMeteor, ArtificialMeteorDto>
    {
        static ArtificialMeteorDtoFetcher()
        {
            CreateMapForIndex();
            CreateMapForList();
            CreateMapForCard();
        }

        private static void CreateMapForIndex()
        {
            var map = CreateFetchMap(FetchAim.Index);
            MapBaseForIndex(map);
        }

        private static void CreateMapForList()
        {
            var map = CreateFetchMap(FetchAim.List);
            MapBaseForList(map);
            MapSpecificForList(map);
        }

        /// <summary>
        /// Мапит специфические свойства метеорита неприродного происхождения для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchMap<ArtificialMeteor, ArtificialMeteorDto> map)
        {
            map.Map(d => d.SerialNumber, e => e.SerialNumber)
               .Map(d => d.CountryName, e => e.Country.Name)
               .Map(d => d.QualityEngineerName, e => e.QualityEngineer.FullName)
               .Map(d => d.MakerName, e => e.Maker.Name)
               .Map(d => d.MakerAddress, e => e.Maker.Address)
               .Map(d => d.MakerDirectorName, e => e.Maker.Director.FullName);
        }

        private static void CreateMapForCard()
        {
            var map = CreateFetchMap(FetchAim.Card);
            MapBaseForCard(map);
            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства метеорита неприродного происхождения для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchMap<ArtificialMeteor, ArtificialMeteorDto> map)
        {
            map.Map(d => d.SerialNumber, e => e.SerialNumber)
               .Map(d => d.CountryId, e => e.Country.Id)
               .Map(d => d.QualityEngineerId, e => e.QualityEngineer.Id)
               .Map(d => d.MakerId, e => e.Maker.Id);
        }

        public ArtificialMeteorDtoFetcher(IRepository repository) : base(repository)
        {
        }
    }
}