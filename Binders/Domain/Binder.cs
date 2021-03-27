using System.Collections.Generic;
using System.IO;
using WebGalery.Core.BinderInterfaces;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Binders.Domain
{
    public class Binder : IBinder
    {
        public const string DirectoryBinderType = "DIRECTORY";

        private readonly IHasher _hasher;
        private readonly IBinderEntityRepository _binderRepository;
        private readonly ICurrentDatabaseInfoProvider _dbInfoProvider;

        public Binder(
            IHasher hasher,
            IBinderEntityRepository binderRepository,
            ICurrentDatabaseInfoProvider dbInfoProvider
            )
        {
            _hasher = hasher;
            _binderRepository = binderRepository;
            _dbInfoProvider = dbInfoProvider;
        }

        public BinderEntity GetDirectoryBinderForPath(string fullPath)
        {
            var subPath = _dbInfoProvider.CurrentInfo.ActiveRack.GetSubpath(fullPath);
            string hash = GetHashForDirectoryBinder(subPath);
            var binder = _binderRepository.Get(hash);
            if (binder == null)
            {
                binder = CreateNew(hash, fullPath, DirectoryBinderType);
                binder.RackHash = _dbInfoProvider.CurrentInfo.ActiveRack.Hash;
            }
            return binder;
        }

        private string GetHashForDirectoryBinder(string subPath)
        {
            string rackHash = _dbInfoProvider.CurrentInfo.ActiveRack.Hash;
            string universalSubPath = CreateUniversalSubPath(subPath);
            string hash = _hasher.ComputeStringHash($"{rackHash}.{universalSubPath}");
            return hash;
        }

        public static string CreateUniversalSubPath(string subPath)
        {
            string retVal = subPath.Replace(Path.DirectorySeparatorChar, '$');
            retVal = retVal.Replace(Path.AltDirectorySeparatorChar, '$');
            retVal = retVal.Replace('\\', '$');
            retVal = retVal.Replace('/', '$');
            return retVal;
        }

        private BinderEntity CreateNew(string hash, string label, string type)
        {
            BinderEntity binderEntity = new BinderEntity
            {
                Hash = hash,
                Label = label,
                Type = type,
                AttributedContents = new List<AttributedBinderEntityToContentEntity>(),
                Contents = new List<BinderEntityToContentEntity>(),
            };

            return binderEntity;
        }
    }
}
