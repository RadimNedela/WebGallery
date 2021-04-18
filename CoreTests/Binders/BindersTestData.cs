using NSubstitute;
using WebGalery.Core.Binders;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Tests.Binders
{
    public class BindersTestData
    {
        public Binder CreateTestBinder()
        {
            CoreTestData ctd = new();

            IHasher hasher = ctd.CreateTestHasher();
            IBinderEntityRepository binderRepository = CreateTestBinderRepository();
            ICurrentDatabaseInfoProvider currentDatabaseInfoProvider = ctd.CreateTestCurrentDatabaseInfoProvider();

            Binder binder = new(hasher, binderRepository, currentDatabaseInfoProvider);
            return binder;
        }

        public IBinderEntityRepository CreateTestBinderRepository()
        {
            IBinderEntityRepository repository = Substitute.For<IBinderEntityRepository>();
            var testBinder = new BinderEntity() { };
            repository.Get(Arg.Any<string>()).Returns(testBinder);

            return repository;
        }
    }
}
