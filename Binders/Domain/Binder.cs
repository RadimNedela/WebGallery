using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Binders.Domain
{
    public class Binder : IBinder
    {
        private readonly IHasher hasher;
        private readonly IBinderEntityRepository binderRepository;

        public Binder(
            IHasher hasher,
            IBinderEntityRepository binderRepository
            )
        {
            this.hasher = hasher;
            this.binderRepository = binderRepository;
        }

        public BinderEntity GetDirectoryBinderForPath(string path)
        {
            return null;
        }
    }
}
