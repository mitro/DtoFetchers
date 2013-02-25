using Autofac;
using DtoFetchers.DataAccess;
using DtoFetchers.DataAccess.DtoFetchers;
using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Регистрируем зависимости в Autofac
            var container = RegisterComponents();

            var testService = container.Resolve<TestService>();

            testService.Run();
        }

        private static IContainer RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestService>()
                .AsSelf();

            builder.RegisterType<NhUnitOfWorkFactory>()
                .As<IUnitOfWorkFactory>().SingleInstance();

            builder.RegisterType<NhRepository>()
                .As<IRepository>().SingleInstance();

            // Просим фабрику создать экземпляр Unit Of Work при каждом разрешении зависимости
            builder.Register(c =>
                {
                    var uowFactory = c.Resolve<IUnitOfWorkFactory>();
                    return uowFactory.Create();
                })
                .As<IUnitOfWork>();

            builder.RegisterType<SpaceMeteorDtoFetcher>()
                   .As<IDtoFetcher<SpaceMeteor, SpaceMeteorDto>>();

            builder.RegisterType<ArtificialMeteorDtoFetcher>()
                   .As<IDtoFetcher<ArtificialMeteor, ArtificialMeteorDto>>();

            return builder.Build();
        }
    }
}
