using WebGalery.Core.DbEntities.Contents;

namespace WebGalery.Core.Binders
{
    public interface IBinderFactory
    {
        Binder GetOrBuildDirectoryBinderForPath(string fullPath);
    }
}
