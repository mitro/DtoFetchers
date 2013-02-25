using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers.DataAccess.DtoFetchers
{
    /// <summary>
    /// Базовый фетчер DTO метеорита.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public class BaseMeteorDtoFetcher<TEntity, TDto> : BaseDtoFetcher<TEntity, TDto>
        where TEntity : Meteor
        where TDto : MeteorDto
    {
        /// <summary>
        /// Мапит базовые свойства метеорита для индекса.
        /// </summary>
        /// <param name="map">Объект мапинга для индекса</param>
        protected static void MapBaseForIndex(IFetchMap<TEntity, TDto> map)
        {
            map.Map(d => d.Id, e => e.Id)
               .Map(d => d.Name, e => e.Name);
        }

        /// <summary>
        /// Мапит базовые свойства метеорита для списка.
        /// </summary>
        /// <param name="map">Объект мапинга для списка</param>
        protected static void MapBaseForList(IFetchMap<TEntity, TDto> map)
        {
            map.Map(d => d.Id, e => e.Id)
               .Map(d => d.Name, e => e.Name)
               .Map(d => d.Weight, e => e.Weight)
               .Map(d => d.DistanceToEarth, e => e.DistanceToEarth)
               .Map(d => d.RiskLevelName, e => e.RiskLevel.Name)
               .Map(d => d.MaterialName, e => e.Material.Name);
        }

        /// <summary>
        /// Мапит базовые свойства метеорита для карточки.
        /// </summary>
        /// <param name="map">Объект мапинга для карточки</param>
        protected static void MapBaseForCard(IFetchMap<TEntity, TDto> map)
        {
            map.Map(d => d.Id, e => e.Id)
               .Map(d => d.Name, e => e.Name)
               .Map(d => d.Weight, e => e.Weight)
               .Map(d => d.DistanceToEarth, e => e.DistanceToEarth)
               .Map(d => d.RiskLevelId, e => e.RiskLevel.Id)
               .Map(d => d.MaterialId, e => e.Material.Id);
        }

        protected BaseMeteorDtoFetcher(IRepository repository) : base(repository)
        {
        }
    }
}