using WebGalery.Domain.Databases;
using WebGalery.Domain.IoC;

namespace WebGalery.Domain
{
    public class Session
    {
        public Database CurrentDatabase { get; set; }

        public Session(Database? currentDatabase = null)
        {
            CurrentDatabase = currentDatabase ?? IoCDefaults.DatabaseFactory.Create();
        }
    }
}
