using System;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.FileImport.Domain
{
    public class Binder
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

        internal BinderEntity GetDirectoryBinderForPhysicalFile(PhysicalFile physicalFile)
        {
            return null;
        }
    }
}
