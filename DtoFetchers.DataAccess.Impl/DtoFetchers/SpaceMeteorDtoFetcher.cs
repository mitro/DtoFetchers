using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers.DataAccess.DtoFetchers
{
    /// <summary>
    /// Фетчер DTO космических метеоритов.
    /// </summary>
    public class SpaceMeteorDtoFetcher: BaseMeteorDtoFetcher<SpaceMeteor, SpaceMeteorDto>
    {
        static SpaceMeteorDtoFetcher()
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
        /// Мапит специфические свойства космического метеорита для списка.
        /// </summary>
        /// <param name="map">Объект маппинга для списка</param>
        private static void MapSpecificForList(IFetchMap<SpaceMeteor, SpaceMeteorDto> map)
        {
            map.Map(d => d.DetectedAt, e => e.DetectedAt)
               .Map(d => d.DetectingPersonName, e => e.DetectingPerson.FullName)
               .Map(d => d.PlaceOfOriginName, e => e.PlaceOfOrigin.Name);
        }

        private static void CreateMapForCard()
        {
            var map = CreateFetchMap(FetchAim.Card);
            MapBaseForCard(map);
            MapSpecificForCard(map);
        }

        /// <summary>
        /// Мапит специфические свойства космического метеорита для карточки.
        /// </summary>
        /// <param name="map">Объект маппинга для карточки</param>
        private static void MapSpecificForCard(IFetchMap<SpaceMeteor, SpaceMeteorDto> map)
        {
            map.Map(d => d.DetectedAt, e => e.DetectedAt)
               .Map(d => d.DetectingPersonId, e => e.DetectingPerson.Id)
               .Map(d => d.PlaceOfOriginId, e => e.PlaceOfOrigin.Id);
        }

        public SpaceMeteorDtoFetcher(IRepository repository) : base(repository)
        {
        }
    }
}