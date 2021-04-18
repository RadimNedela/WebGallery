using NSubstitute;
using WebGalery.Binders.Domain;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;
using WebGalery.Core.Tests;

namespace WebGalery.PictureViewer.Tests
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
