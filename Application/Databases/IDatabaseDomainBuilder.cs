using WebGalery.Domain.Databases;

namespace Application.Databases
{
    public interface IDatabaseDomainBuilder
    {
        Database BuildDomain(DatabaseDto databaseDto);
    }
}
