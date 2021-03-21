using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Binders.Domain
{
    public class Binder : IBinder
    {
        private readonly IHasher _hasher;
        private readonly IBinderEntityRepository _binderRepository;

        public Binder(
            IHasher hasher,
            IBinderEntityRepository binderRepository
            )
        {
            _hasher = hasher;
            _binderRepository = binderRepository;
        }

        public BinderEntity GetDirectoryBinderForPath(string path)
        {
            return null;
        }
    }
}
