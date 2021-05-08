using System.Collections.Generic;
using System.IO;
using WebGalery.Core.DbEntities.Contents;
using WebGalery.Core.DBMaintenanceInterfaces;
using WebGalery.Core.InfrastructureInterfaces;

namespace WebGalery.Core.Binders
{
    public class BinderFactory
    {
        /// <summary>
        /// Eventhough I know that you can accually write $ into file name, for our purposes it could be sufficient
        /// to just ignore special "hacker" cases where somebody will try to create file name colliding with 
        /// directory path. Do not do that. It will not work, I know it and I will ignore such bugs.
        /// </summary>
        public const char UniversalPathSeparator = '$';
        #region Construct
        private readonly IHasher _hasher;
        private readonly IBinderRepository _binderRepository;
        private readonly IActiveRackService _activeRackServices;

        public BinderFactory(
            IHasher hasher,
            IBinderRepository binderRepository,
            IActiveRackService activeRackServices
            )
        {
            _hasher = hasher;
            _binderRepository = binderRepository;
            _activeRackServices = activeRackServices;
        }
        #endregion

        public Binder GetOrBuildDirectoryBinderForPath(string fullPath)
        {
            string hash = GetHashForDirectory(fullPath);
            var binder = _binderRepository.Get(hash);
            if (binder == null)
            {
                binder = CreateNew(hash, fullPath, Binder.DirectoryBinderType);
                binder.RackHash = _activeRackServices.ActiveRackHash;
            }
            return binder;
        }

        public static string CreateUniversalPath(string path)
        {
            string retVal = path.Replace(Path.DirectorySeparatorChar, UniversalPathSeparator);
            retVal = retVal.Replace(Path.AltDirectorySeparatorChar, UniversalPathSeparator);
            retVal = retVal.Replace('\\', UniversalPathSeparator);
            retVal = retVal.Replace('/', UniversalPathSeparator);
            return retVal;
        }

        private string GetHashForDirectory(string fullPath)
        {
            var subPath = _activeRackServices.GetSubpath(fullPath);
            string rackHash = _activeRackServices.ActiveRackHash;
            string universalSubPath = CreateUniversalPath(subPath);
            string hash = _hasher.ComputeStringHash($"{rackHash}.{universalSubPath}");
            return hash;
        }

        private Binder CreateNew(string hash, string label, string type)
        {
            Binder binder = new Binder
            {
                Hash = hash,
                Label = label,
                Type = type,
                AttributedContents = new List<AttributedBinderToContent>(),
                Contents = new List<BinderToContent>(),
            };

            return binder;
        }
    }
}
