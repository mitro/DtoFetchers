using System;
using System.Collections.Generic;
using System.Linq;
using DtoFetchers.DataAccess.Extensions;
using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers.DataAccess.DtoFetchers
{
    /// <summary>
    /// Базовый класс механизма извлечения DTO из хранилища.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущностей, находящихся в хранилище</typeparam>
    /// <typeparam name="TDto">Тип извлекаемых DTO</typeparam>
    public abstract class BaseDtoFetcher<TEntity, TDto> : IDtoFetcher<TEntity, TDto>
        where TEntity : Entity
        where TDto : BaseDto
    {
        // Репозиторий
        private readonly IRepository _repository;

        // Словарь маппингов. Ключ - цель извлечения, для которой создан маппинг
        private static readonly IDictionary<FetchAim, FetchMap<TEntity, TDto>> Maps;

        static BaseDtoFetcher()
        {
            Maps = new Dictionary<FetchAim, FetchMap<TEntity, TDto>>();
        }

        protected BaseDtoFetcher(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TDto> Fetch(
            IQueryable<TEntity> query,
            Page page,
            FetchAim fetchAim)
        {
            var fetchMap = GetFetchMap(fetchAim);

            var dtoList = query
                .Select(fetchMap.SelectExpression)
                .Page(page)
                .ToList();

            var newQuery = _repository.Query<TEntity>();

            fetchMap.CustomFetchOperations.ToList().ForEach(op => op(newQuery, dtoList));

            return dtoList;
        }

        protected static IFetchMap<TEntity, TDto> CreateFetchMap(FetchAim fetchAim)
        {
            if (fetchAim == FetchAim.None)
            {
                throw new ArgumentException("Не указана цель извлечения");
            }

            if (Maps.ContainsKey(fetchAim))
            {
                throw new ArgumentException("Маппинг для указанной цели извлечения уже создан");
            }

            var map = new FetchMap<TEntity, TDto>();

            Maps[fetchAim] = map;

            return map;
        }

        private static FetchMap<TEntity, TDto> GetFetchMap(FetchAim fetchAim)
        {
            if (fetchAim == FetchAim.None)
            {
                throw new ArgumentException("Не указана цель извлечения");
            }

            if (!Maps.ContainsKey(fetchAim))
            {
                throw new ArgumentException("Маппинга для указанной цели извлечения не существует");
            }

            return Maps[fetchAim];
        }
    }
}