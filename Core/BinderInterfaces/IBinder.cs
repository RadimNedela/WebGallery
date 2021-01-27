using WebGalery.Core.DbEntities.Contents;

namespace WebGalery.Core.BinderInterfaces
{
    public interface IBinder
    {
        BinderEntity GetDirectoryBinderForPath(string path);
    }
}
