using WebGalery.Core.DbEntities.Contents;

namespace WebGalery.Binders.Domain
{
    public interface IBinder
    {
        BinderEntity GetDirectoryBinderForPath(string path);
    }
}
