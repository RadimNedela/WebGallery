using NSubstitute;
using WebGalery.Core.Binders;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Tests.Binders
{
    public class BindersTestData
    {
        public BinderFactory CreateTestBinder()
        {
            CoreTestData ctd = new();

            IHasher hasher = ctd.CreateTestHasher();
            IBinderRepository binderRepository = CreateTestBinderRepository();
            IActiveDatabaseInfoProvider currentDatabaseInfoProvider = ctd.CreateTestCurrentDatabaseInfoProvider();

            BinderFactory binder = new(hasher, binderRepository, currentDatabaseInfoProvider);
            return binder;
        }

        public IBinderRepository CreateTestBinderRepository()
        {
            IBinderRepository repository = Substitute.For<IBinderRepository>();
            var testBinder = new Binder() { };
            repository.Get(Arg.Any<string>()).Returns(testBinder);

            return repository;
        }
    }
}
