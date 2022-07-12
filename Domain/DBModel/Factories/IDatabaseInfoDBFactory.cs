using WebGalery.Domain.Databases;

namespace WebGalery.Domain.DBModel.Factories
{
    public interface IDatabaseInfoDBFactory
    {
        DatabaseInfoDB Build(Database database);
    }
}