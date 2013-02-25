using System;
using DtoFetchers.DataAccess;
using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers
{
    public class TestService
    {
        // Репозиторий
        private readonly IRepository _repository;

        // Фетчер DTO космических метеоритов
        private readonly IDtoFetcher<SpaceMeteor, SpaceMeteorDto> _spaceMeteorDtoFetcher;

        // Фетчер DTO метеоритов некосмического происхождения
        private readonly IDtoFetcher<ArtificialMeteor, ArtificialMeteorDto> _artificialMeteorDtoFetcher;

        public TestService(IRepository repository,
            IDtoFetcher<SpaceMeteor, SpaceMeteorDto> spaceMeteorDtoFetcher,
            IDtoFetcher<ArtificialMeteor, ArtificialMeteorDto> artificialMeteorDtoFetcher)
        {
            _repository = repository;
            _spaceMeteorDtoFetcher = spaceMeteorDtoFetcher;
            _artificialMeteorDtoFetcher = artificialMeteorDtoFetcher;
        }

        public void Run()
        {
            var spaceMeteorQuery = _repository.Query<SpaceMeteor>();
            var artificialMeteorQuery = _repository.Query<ArtificialMeteor>();

            var indexSpaceMeteors = _spaceMeteorDtoFetcher.Fetch(spaceMeteorQuery, Page.All, FetchAim.Index);
            var indexArtificialMeteors = _artificialMeteorDtoFetcher.Fetch(artificialMeteorQuery, Page.All, FetchAim.Index);
            
            var listSpaceMeteors = _spaceMeteorDtoFetcher.Fetch(spaceMeteorQuery, Page.All, FetchAim.List);
            var listArtificialMeteors = _artificialMeteorDtoFetcher.Fetch(artificialMeteorQuery, Page.All, FetchAim.List);
            
            var cardSpaceMeteors = _spaceMeteorDtoFetcher.Fetch(spaceMeteorQuery, Page.All, FetchAim.Card);
            var cardArtificialMeteors = _artificialMeteorDtoFetcher.Fetch(artificialMeteorQuery, Page.All, FetchAim.Card);

            Console.WriteLine("Сгенерированные SQL-запросы записаны в файл debug.txt в папке приложения");
            Console.WriteLine("Установите точку останова, чтобы просмотреть инициализированные поля DTO");
            Console.ReadLine();
        }
    }
}